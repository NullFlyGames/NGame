using System.Runtime.CompilerServices;

namespace NGame.Event
{
    public interface IAsyncEvent : IEventAsync, INotifyCompletion
    {
        IAsyncEvent GetAwaiter();
        object GetResult();
    }
}
