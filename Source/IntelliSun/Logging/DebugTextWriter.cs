using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IntelliSun.Logging
{
    public class DebugTextWriter : TextWriter
    {
        private static readonly DebugTextWriter _instance = new DebugTextWriter();

        /// <summary>
        ///     Writes a subarray of characters to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from. </param>
        /// <param name="index">The character position in the buffer at which to start retrieving data. </param>
        /// <param name="count">The number of characters to write. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The buffer length minus <paramref name="index" /> is less than
        ///     <paramref name="count" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> or <paramref name="count" /> is
        ///     negative.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Write(char[] buffer, int index, int count)
        {
            var stringValue = String.Concat(buffer.Skip(index).Take(count));
            Debug.Write(stringValue);
        }

        /// <summary>
        ///     Gets the character encoding in which the output is written.
        /// </summary>
        /// <returns>
        ///     The character encoding in which the output is written.
        /// </returns>
        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        public static DebugTextWriter Instance
        {
            get { return _instance; }
        }
    }
}