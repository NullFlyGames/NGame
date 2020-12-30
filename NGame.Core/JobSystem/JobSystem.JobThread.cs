using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core.Jobs
{
    public partial class JobSystem
    {
        public enum ThreadState
        {
            Slep,
            Runing,
        }
        /// <summary>
        /// 工作线程
        /// </summary>
        public class JobThread 
        {
            public int Thread_Id;
            public ThreadState State;
            public bool isLoop = false;
            public float delaytime = 0;
            public float attachtime = 0;
            private Action m_handle;
            private Action m_action;

            public void Awake()
            {
                Thread_Id = GetHashCode();
                State = ThreadState.Slep;
            }
            public void QueueUserWorkItem(Action handle, float time, bool isloop)
            {
                m_action = handle;
                delaytime = time;
                isLoop = isloop;
                attachtime = realtimeSinceStartup;
                
                State = ThreadState.Runing;
            }

          
            public void CancelRuning()
            {
                State = ThreadState.Slep;
                m_handle = null;
                m_action = null;
            }

            public void FixedUpdate()
            {
                if (State == ThreadState.Slep)
                    return;
                if (realtimeSinceStartup - attachtime >= delaytime)
                {
                    m_handle?.Invoke();
                    m_action?.Invoke();
                    attachtime = realtimeSinceStartup;
    
                    State = isLoop ? ThreadState.Runing : ThreadState.Slep;
                    if (isLoop == false)
                    {
                        CancelQueueUserWorkItem(Thread_Id);
                    }
                }
            }

            public void Release()
            {
                CancelRuning();
                attachtime = 0;
                delaytime = 0;
                isLoop = false;
            }
        }
    }
}
