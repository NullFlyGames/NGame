using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core
{
    /// <summary>
    /// 逻辑系统接口
    /// </summary>
    /// <typeparam name="Mathce"></typeparam>
    public interface IExecuteSystem<Mathce> : ExecuteSystem where Mathce : class, IMatcher
    {
        Mathce Mathces { get; }
    }

    /// <summary>
    /// 逻辑系统接口
    /// </summary>
    /// <typeparam name="Mathce"></typeparam>
    public interface ExecuteSystem : ISystem
    {
        void Execute();
    }
}
