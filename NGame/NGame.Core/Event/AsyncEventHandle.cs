using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Event
{
    public sealed class AsyncEventHandle : IAsyncEvent
    {
        public int id { get; set; }
        public bool IsCompleted { get; private set; }
        public float starTime { get; set; }
        public float outTime { get; set; }
        object result;
        Action _continuation;

        public IAsyncEvent GetAwaiter()
        {
            return this;
        }

        public object GetResult()
        {
            return result;
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
            result = o;
            _continuation();
        }
    }
}
