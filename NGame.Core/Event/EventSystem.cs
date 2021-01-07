using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;
namespace NGame.Event
{
    public sealed class EventSystem : IManaged
    {
        public static IEvent CurEvent { get; private set; }
        private float curTime;
        private static Dictionary<string, IEvent> _events;
        private static Dictionary<int, IEventAsync> _async;

        public void Broadcast(IEventArgs args)
        {
            if (_events.ContainsKey(args.name))
            {
                _events[args.name](args);
            }
        }

        public void SetAsyncEventResult(int id, object o)
        {
            if (_async.ContainsKey(id))
            {
                _async[id].SetResult(o);
                return;
            }
        }
        public AsyncEventHandle OnCreate(int id, float outTime = 5f)
        {
            AsyncEventHandle handle = new AsyncEventHandle();
            handle.id = id;
            handle.starTime = curTime;
            handle.outTime = outTime;
            if (_async.ContainsKey(id))
            {
                _async[id] = handle;
            }
            else
            {
                _async.Add(id, handle);
            }
            return handle;
        }
        public AsyncEventHandle<T> OnCreate<T>(int id, float outTime = 5f)
        {
            AsyncEventHandle<T> handle = new AsyncEventHandle<T>();
            handle.id = id;
            handle.starTime = curTime;
            handle.outTime = outTime;
            if (_async.ContainsKey(id))
            {
                _async[id] = handle;
            }
            else
            {
                _async.Add(id, handle);
            }
            return handle;
        }
        public void ListenerEvent(IEvent @event)
        {
            if (_events.ContainsKey(nameof(@event)))
                return;
            _events.Add(nameof(@event), @event);
        }
        public void Initlizition()
        {
            _events = new Dictionary<string, IEvent>();
            _async = new Dictionary<int, IEventAsync>();
        }

        public void Update(float time)
        {
            curTime = time;
            foreach (var item in _async)
            {
                if (curTime - item.Value.starTime > item.Value.outTime && item.Value.IsCompleted == false)
                {
                    item.Value.SetResult(null);
                }
            }
        }

        public void Dispose()
        {
            _events.Clear();
            _async.Clear();
        }
    }
}
