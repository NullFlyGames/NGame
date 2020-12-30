using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 数据编码
    /// </summary>
    public interface IEncoderComparerChannel
    {
        void Encoder(IChannel context, IMemory momery);
    }
}
