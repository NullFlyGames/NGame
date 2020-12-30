using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Event
{
    public sealed class EventSystem
    {
        public static IEvent CurEvent { get; private set; }
        private static Queue<IEvent> Events = new Queue<IEvent>();

        public static void Broadcast(IEvent eve)
        {
            Events.Enqueue(eve);
        }

        internal static void FixedUpdate(float time)
        {
            if (Events.Count <= 0) return;

            if (CurEvent == null) CurEvent = Events.Dequeue();

            if (CurEvent.IsCompleted == false) return;

            CurEvent = null;
        }
        internal static void Dispose()
        { 
        
        }
    }
}
