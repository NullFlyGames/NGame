﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    /// <summary>
    /// 逻辑系统接口
    /// </summary>
    /// <typeparam name="Mathce"></typeparam>
    public interface ExecuteSystem<Mathce> : ISystem where Mathce : class, IMatcher
    {
        Mathce Mathces { get; }
        void Execute();

    }
}
