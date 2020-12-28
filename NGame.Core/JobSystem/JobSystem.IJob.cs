using System;
using System.Runtime.CompilerServices;

namespace NGame.Core.Jobs
{
    /// <summary>
    /// �첽����ӿ�
    /// </summary>
    /// <typeparam name="Job"></typeparam>
    /// <typeparam name="Result"></typeparam>
    public interface IJob : INotifyCompletion
    {
        /// <summary>
        /// ����ID
        /// </summary>
        int Id { get; set; }
        float OutTime { get; set; }
        object Owner { get; set; }
        bool isRelease { get; }
        Exception Exception { get; set; }
        /// <summary>
        /// �����Ƿ����
        /// </summary>
        bool IsCompleted { get; }

        void SetResult(object o);
    }
}
