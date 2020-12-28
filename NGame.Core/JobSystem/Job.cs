using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core.Jobs
{
    /// <summary>
    /// 无返回值异步任务
    /// </summary>
    public class Job : IJob
    {
        private Action m_continuation;
        private object m_result;
        private float __out;
        /// <summary>
        /// 任务ID
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 任务是否完成
        /// </summary>
        public bool IsCompleted
        {
            get;
            private set;
        }
        public Exception Exception
        {
            get;
            set;
        }
        public float OutTime
        {
            get => __out;
            set => __out = JobSystem.realtimeSinceStartup + value;
        }
        public object Owner
        {
            get;
            set;
        }
        public bool isRelease
        {
            get;
            private set;
        }


        /// <summary>
        /// 获取异步对象
        /// </summary>
        /// <returns></returns>
        public Job GetAwaiter()
        {
            return this;
        }
        /// <summary>
        /// 获取异步结果
        /// </summary>
        /// <returns></returns>
        public object GetResult()
        {
            return this;
        }
        /// <summary>
        /// 设置异步结果
        /// </summary>
        /// <param name="o"></param>
        public void SetResult(object o)
        {
            m_result = o;
            if (m_continuation == null) return;

            m_continuation();
            isRelease = true;
        }
        /// <summary>
        /// 完成事件
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
        /// 回收异步对象
        /// </summary>
        public void Release()
        {
            m_continuation = null;
            IsCompleted = false;
            m_result = null;
            isRelease = false;
            OutTime = 0;
        }
    }
}
