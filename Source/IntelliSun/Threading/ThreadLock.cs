using System;
using System.Threading;

namespace IntelliSun.Threading
{
    public abstract class ThreadLock : IThreadLock
    {
        public abstract ILockHandler LockRead();
        public abstract ILockHandler LockRead(bool blocking);

        public abstract ILockHandler LockWrite();
        public abstract ILockHandler LockWrite(bool blocking);

        public static ThreadLock Create()
        {
            return new SlimThreadLock();
        }
    }

    internal class SlimThreadLock : ThreadLock
    {
        private const LockRecursionPolicy DefaultRecursionPolicy =
            LockRecursionPolicy.NoRecursion;

        private readonly ReaderWriterLockSlim lockInstance;

        public SlimThreadLock()
        {
            this.lockInstance = new ReaderWriterLockSlim(DefaultRecursionPolicy);
        }

        public override ILockHandler LockRead()
        {
            return this.LockRead(true);
        }

        public override ILockHandler LockRead(bool blocking)
        {
            return this.SouldExecuteLock(false)
                ? StaticLockHandler.Instance
                : SlimThreadLockHandler.ReadLock(this.lockInstance, blocking);
        }

        public override ILockHandler LockWrite()
        {
            return this.LockWrite(true);
        }

        public override ILockHandler LockWrite(bool blocking)
        {
            return this.SouldExecuteLock(true)
                ? StaticLockHandler.Instance
                : SlimThreadLockHandler.WriteLock(this.lockInstance, blocking);
        }

        private bool SouldExecuteLock(bool asWrite)
        {
            return (asWrite && this.lockInstance.IsWriteLockHeld) ||
                   (!asWrite && this.lockInstance.IsReadLockHeld ||
                    this.lockInstance.IsUpgradeableReadLockHeld ||
                    this.lockInstance.IsWriteLockHeld);
        }
    }

    public interface IThreadLock
    {
        ILockHandler LockRead();
        ILockHandler LockRead(bool blocking);

        ILockHandler LockWrite();
        ILockHandler LockWrite(bool blocking);
    }

    public interface ILockHandler : IDisposable
    {
        void Exit();

        bool IsAcquired { get; }
    }

    internal class StaticLockHandler : ILockHandler
    {
        private static readonly ILockHandler _instance;

        static StaticLockHandler()
        {
            _instance = new StaticLockHandler();
        }

        public void Exit()
        {
            
        }

        public void Dispose()
        {
            
        }

        public bool IsAcquired
        {
            get { return true; }
        }

        public static ILockHandler Instance
        {
            get { return StaticLockHandler._instance; }
        }
    }

    internal class SlimThreadLockHandler : ILockHandler
    {
        private readonly ReaderWriterLockSlim lockInstance;
        private readonly bool isWriteLock;

        private SlimThreadLockHandler(ReaderWriterLockSlim lockInstance, bool isWriteLock, bool blocking)
        {
            this.lockInstance = lockInstance;
            this.isWriteLock = isWriteLock;

            this.Enter(blocking);
        }

        public void Exit()
        {
            if (!this.IsAcquired)
                return;

            this.ExitCore();
            this.IsAcquired = false;
        }

        private void ExitCore()
        {
            if (this.isWriteLock)
                this.lockInstance.ExitWriteLock();
            else
                this.lockInstance.ExitReadLock();
        }

        private void Enter(bool blocking)
        {
            this.IsAcquired = blocking
                ? this.EnterBlocking()
                : this.EnterNonBlocking();
        }

        private bool EnterNonBlocking()
        {
            return this.isWriteLock
                ? this.lockInstance.TryEnterWriteLock(0)
                : this.lockInstance.TryEnterReadLock(0);
        }

        private bool EnterBlocking()
        {
            if (this.isWriteLock)
                this.lockInstance.EnterWriteLock();
            else
                this.lockInstance.EnterReadLock();

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Exit();
        }

        public static SlimThreadLockHandler ReadLock(ReaderWriterLockSlim locker, bool blocking)
        {
            return new SlimThreadLockHandler(locker, false, blocking);
        }

        public static SlimThreadLockHandler WriteLock(ReaderWriterLockSlim locker, bool blocking)
        {
            return new SlimThreadLockHandler(locker, true, blocking);
        }

        public bool IsAcquired { get; private set; }
    }
}
