using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 消息处理管道
    /// </summary>
    public interface IHandle
    {
        void ConnectdCompleted(int id);
        void DisConnectd(int id);
        void HandleMessage(int id, IRPC rPC);
    }
}
