using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 数据解码
    /// </summary>
    public interface IDecoderComparserChannel
    {
        void Decoder(IChannel context, IMemory momery);
    }
}
