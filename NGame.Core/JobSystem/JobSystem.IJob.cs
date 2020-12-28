using System;
using System.Runtime.CompilerServices;

namespace NGame.Core.Jobs
{
    /// <summary>
    /// 异步任务接口
    /// </summary>
    /// <typeparam name="Job"></typeparam>
    /// <typeparam name="Result"></typeparam>
    public interface IJob : INotifyCompletion
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        int Id { get; set; }
        float OutTime { get; set; }
        object Owner { get; set; }
        bool isRelease { get; }
        Exception Exception { get; set; }
        /// <summary>
        /// 任务是否完成
        /// </summary>
        bool IsCompleted { get; }

        void SetResult(object o);
    }
}
