using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Event
{
    public class AsyncEventHandle<T> : IAsyncEvent<T>
    {
        public int id { get; set; }
        public bool IsCompleted { get; private set; }
        public float starTime { get; set; }
        public float outTime { get; set; }
        object result;
        Action _continuation;
        public IAsyncEvent<T> GetAwaiter()
        {
            return this;
        }

        public T GetResult()
        {
            if (result == null)
                return default;
            T m = (T)result;
            return m;
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            if (IsCompleted == false)
                return;
            IsCompleted = true;
            _continuation();
        }

        public void SetResult(object o)
        {
            IsCompleted = true;
            this.result = o;
            if (_continuation != null)
                _continuation();
        }
    }
}
