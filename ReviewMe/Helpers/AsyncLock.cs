using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe.Helpers
{
    //https://www.codeproject.com/Questions/1073406/How-to-make-await-async-functions-thread-safe
    public class AsyncLock : IAsyncLock
    {
        private readonly Queue<TaskCompletionSource<IDisposable>> _waiters = new Queue<TaskCompletionSource<IDisposable>>();
        private readonly WaitCallback _releaseCoreCallback;
        private readonly Task<IDisposable> _releaserTask;
        private readonly Releaser _releaser;
        private readonly int _maxCount;
        private int _currentCount;

        public AsyncLock(int initialCount = 1)
        {
            if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");

            _maxCount = _currentCount = initialCount;

            _releaser = new Releaser(this);
            _releaserTask = Task.FromResult((IDisposable)_releaser);
            _releaseCoreCallback = ReleaseCore;
        }

        public Task<IDisposable> LockAsync()
        {
            lock (_waiters)
            {
                if (_currentCount > 0)
                {
                    _currentCount--;
                    return _releaserTask;
                }

                var waiter = new TaskCompletionSource<IDisposable>();
                _waiters.Enqueue(waiter);
                return waiter.Task;
            }
        }

        private void Release()
        {
            TaskCompletionSource<IDisposable> toRelease = null;
            lock (_waiters)
            {
                if (_waiters.Count > 0)
                {
                    toRelease = _waiters.Dequeue();
                }
                else if (_currentCount < _maxCount)
                {
                    _currentCount++;
                }
                else
                {
                    throw new SemaphoreFullException();
                }
            }
            if (null != toRelease)
            {
                ThreadPool.QueueUserWorkItem(_releaseCoreCallback, toRelease);
            }
        }

        private void ReleaseCore(object state)
        {
            ((TaskCompletionSource<IDisposable>)state).SetResult(_releaser);
        }

        private sealed class Releaser : IDisposable
        {
            private readonly AsyncLock _throttle;

            public Releaser(AsyncLock throttle)
            {
                _throttle = throttle;
            }

            public void Dispose()
            {
                _throttle.Release();
            }
        }
    }
}