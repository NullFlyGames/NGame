using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Event
{
    public interface IEvent
    {
        int id { get; set; }
        bool IsCompleted { get; }
    }
    public interface IEventAsync : IEvent, INotifyCompletion
    {
        void SetResult(object o);
        IEvent GetAwaiter();
        object GetResult();
    }
    public interface IEventAsync<T> : IEvent, INotifyCompletion
    {
        void SetResult(object o);
        IEventAsync<T> GetAwaiter();
        T GetResult();
        void SetResult(T result);
    }
}
