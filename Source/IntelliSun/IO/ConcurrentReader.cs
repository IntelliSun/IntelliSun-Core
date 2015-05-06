using System;
using System.IO;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.IO
{
    public class ConcurrentReader : Unmanaged
    {
        private readonly Func<Stream> factory;
        private readonly int capacity;

        private readonly Stream[] permanents;
        private readonly RollStream[] rollStreams;

        private BinaryReader permanentReader;

        public ConcurrentReader(Func<Stream> factory)
            : this(factory, 1, 1)
        {
            
        }

        public ConcurrentReader(Func<Stream> factory, int capacity)
            : this(factory, capacity, 1)
        {
        }

        public ConcurrentReader(Func<Stream> factory, int capacity, int permanents)
        {
            this.factory = factory;
            this.capacity = capacity;

            this.rollStreams = EnumerationBuilder.RepeatUnique(() => new RollStream(factory()), capacity)
                                                 .ToArray();

            this.permanents = EnumerationBuilder.RepeatUnique(factory, permanents)
                                                .ToArray();
        }

        public Stream Get()
        {
            var stream = this.rollStreams.FirstOrDefault(s => s.IsFree);
            return stream ?? this.GetOnce();
        }

        public void Free(Stream stream)
        {
            stream.Dispose();
        }

        public Stream GetOnce()
        {
            using (var stream = factory())
                return stream;
        }

        public Stream GetUnmanaged()
        {
            return factory();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var permanent in permanents)
                permanent.Dispose();

            foreach (var rollStream in rollStreams)
                rollStream.DisposeAll();

            base.Dispose(disposing);
        }

        public Stream Permanent
        {
            get { return this.permanents[0]; }
        }

        public BinaryReader PermanentReader
        {
            get { return permanentReader ?? (permanentReader = new BinaryReader(this.permanents[0])); }
        }

        public Stream this[int index]
        {
            get
            {
                return index < this.permanents.Length
                    ? this.permanents[index]
                    : this.rollStreams[index - this.permanents.Length];
            }
        }

        public int Capacity
        {
            get { return this.capacity; }
        }
    }

    internal class RollStream : Stream
    {
        private readonly Stream streamBase;

        public RollStream(Stream streamBase)
        {
            this.streamBase = streamBase;
        }

        public override void Flush()
        {
            this.streamBase.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.streamBase.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.streamBase.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.streamBase.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.streamBase.Write(buffer, offset, count);
        }

        public new void Dispose()
        {
            this.streamBase.Position = 0;
            this.IsFree = true;
        }

        internal void DisposeAll()
        {
            this.streamBase.Dispose();
            this.Dispose(true);
        }

        public override bool CanRead
        {
            get { return this.streamBase.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.streamBase.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return this.streamBase.Length; }
        }

        public override long Position
        {
            get { return this.streamBase.Position; }
            set { this.streamBase.Position = value; }
        }

        internal bool IsFree { get; set; }
    }
}
