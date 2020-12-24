using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NSocket.Message
{
    /// <summary>
    /// 消息处理管道
    /// </summary>
    public abstract class Handle : NCore.IEntity
    {
        public abstract int id { get; set; }

        public abstract void Awake();
        public abstract void DisConnect();
        public abstract void Dispose();
    }
}
