using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core.RPC
{
    /// <summary>
    /// RPC消息结构
    /// </summary>
    interface IMessaged
    {
        IMessageHead Head { get; }
    }
}
