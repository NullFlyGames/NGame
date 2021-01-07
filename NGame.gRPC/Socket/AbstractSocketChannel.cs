using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using NGame.Event;

namespace NGame.NRPC
{
    public abstract class AbstractSocketChannel : ITcpSocket
    {
        static int index = 0;
        public int id => ++index;

        protected Socket _sock;
        public IPEndPoint Adders => (IPEndPoint)_sock.RemoteEndPoint;

        public IPEndPoint Local => (IPEndPoint)_sock.LocalEndPoint;

        /// <summary>
        /// 释放当前对象
        /// </summary>
        public virtual void Dispose() { }
        /// <summary>
        /// 开始监听网络连接
        /// </summary>
        /// <param name="o"></param>
        public virtual void DoAccept(object o) { }
        /// <summary>
        /// 开始连接远程地址
        /// </summary>
        /// <param name="o"></param>
        public virtual void DoConnectd(IPEndPoint adder) { }
        /// <summary>
        /// 开始接收数据
        /// </summary>
        /// <param name="o"></param>
        public virtual void DoRecvie(object o) { }
        /// <summary>
        /// 开始发送数据
        /// </summary>
        /// <param name="o"></param>
        public virtual void DoSend(object o) { }
        /// <summary>
        /// 绑定本地地址，并且开始监听网络连接
        /// </summary>
        /// <param name="adder"></param>
        public virtual void DoBind(IPEndPoint adder) { }
        /// <summary>
        /// 网络连接完成
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnAcceptCompleted(AbstractSocketAsyncEventArgs args) { }
        /// <summary>
        /// 连接远程地址完成
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnConnectdCompleted(AbstractSocketAsyncEventArgs args) { }
        /// <summary>
        /// 数据接收完成
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnRecvieCompleted(AbstractSocketAsyncEventArgs args) { }
        /// <summary>
        /// 数据发送完成
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnSendCompleted(AbstractSocketAsyncEventArgs args) { }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="memory"></param>
        public virtual void Write(IMemory memory) { }
        /// <summary>
        /// 写入数据并发送
        /// </summary>
        /// <param name="memory"></param>
        public virtual void WriteAndFlush(IMemory memory) { }
        /// <summary>
        /// 完成写入
        /// </summary>
        public virtual void Flush() { }
    }
}
