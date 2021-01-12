using System.Runtime.CompilerServices;

namespace NGame.Event
{
    public interface IAsyncEvent<T> : IEventAsync, INotifyCompletion
    {

        IAsyncEvent<T> GetAwaiter();
        T GetResult();
    }
}
