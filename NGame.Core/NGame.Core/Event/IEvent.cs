using System;
namespace NGame.Event
{
    public delegate void IEvent(IEventArgs args);

    public interface IEventAsync
    {
        int id { get; set; }
        float starTime { get; set; }
        float outTime { get; set; }
        bool IsCompleted { get; }
        void SetResult(object o);
    }
}
