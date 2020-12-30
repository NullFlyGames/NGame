using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace NGame.Core.Jobs
{
    public partial class JobSystem
    {
        private static List<JobThread> m_threads = new List<JobThread>();
        public static float realtimeSinceStartup;
    
        public static void FixedUpdate()
        {
            realtimeSinceStartup += 0.01f;

            for (int i = 0; i < m_threads.Count; i++)
            {
                JobThread thread = m_threads[i];
                if (thread == null)
                    continue;
                thread.FixedUpdate();
            }
        }
        public static JobThread GetSlepThread()
        {
            for (int i = 0; i < m_threads.Count; i++)
            {
                JobThread thread = m_threads[i];
                if (thread == null)
                    continue;
                if (m_threads[i].State == ThreadState.Slep)
                    return m_threads[i];
            }
            return null;
        }

        public static JobThread GetJobThread(int id)
        {
            for (int i = 0; i < m_threads.Count; i++)
            {
                if (m_threads[i].Thread_Id == id)
                    return m_threads[i];
            }
            return null;
        }

        static int CreateNewThread(Action handle, float delayTime = 0, bool isLoop = false)
        {
            int id = Guid.NewGuid().GetHashCode();
            JobThread thread = new JobThread();
            thread.Thread_Id = id;
            thread.Awake();
            m_threads.Add(thread);
            thread.QueueUserWorkItem(handle, delayTime, isLoop);
            return id;
        }

        public static int QueueUserWorkItem(Action handle, float delayTime = 0, bool isLoop = false)
        {
            JobThread thread = GetSlepThread();
            if (thread == null)
            {
                return CreateNewThread(handle, delayTime, isLoop);
            }
            thread.QueueUserWorkItem(handle, delayTime, isLoop);
            return thread.Thread_Id;
        }

        public static void CancelQueueUserWorkItem(int id)
        {
            JobThread thread = GetJobThread(id);
            if (thread == null) return;
            thread.CancelRuning();
        }

        public void Release()
        {
            m_threads.Clear();
        }
    }
}
