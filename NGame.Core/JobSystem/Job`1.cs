using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core.Jobs
{
    /// <summary>
    /// �з���ֵ�첽����
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    public class Job<Result> : IJob where Result : class, new()
    {
        private Action m_continuation;
        private float __out;
        /// <summary>
        /// ����ID
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// �첽�����Ƿ����
        /// </summary>
        public bool IsCompleted
        {
            get;
            private set;
        }

        public object Owner
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }
        public float OutTime
        {
            get => __out;
            set
            {
                __out = JobSystem.realtimeSinceStartup + value;
            }
        }
        public bool isRelease
        {
            get;
            private set;
        }

        /// <summary>
        /// �����첽������
        /// </summary>
        /// <param name="o"></param>
        public void SetResult(object o)
        {
            Owner = o as Result;
            IsCompleted = true;
            if (m_continuation == null) return;

            m_continuation();
            isRelease = true;
        }
        /// <summary>
        /// ��ȡ�첽�������
        /// </summary>
        /// <returns></returns>
        public Job<Result> GetAwaiter()
        {
            return this;
        }
        /// <summary>
        /// ��ȡ�첽������
        /// </summary>
        /// <returns></returns>
        public Result GetResult()
        {
            return Owner as Result;
        }
        /// <summary>
        /// ��ɻص�
        /// </summary>
        /// <param name="continuation"></param>
        public void OnCompleted(Action continuation)
        {
            m_continuation = continuation;
            if (IsCompleted == false)
                return;
            m_continuation();
            isRelease = true;
        }


        /// <summary>
        /// �����첽����
        /// </summary>
        public void Release()
        {
            m_continuation = null;
            IsCompleted = false;
        }
    }
}   