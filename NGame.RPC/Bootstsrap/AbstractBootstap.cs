using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public abstract class AbstractBootstap : IBootstrap
    {
        /// <summary>
        /// 远程地址
        /// </summary>
        public int id { get;  set; }
        /// <summary>
        /// 远程地址
        /// </summary>
        public IPEndPoint RemoteAdders { get; protected set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        public IPEndPoint LocalAdders { get; protected set; }
        /// <summary>
        /// 消息处理管道
        /// </summary>
        public IChannelHandle ChannelHandle { get; protected set; }
        /// <summary>
        /// 数据解码组件
        /// </summary>
        public IDecoderComparserChannel DecoderComparser { get; protected set; }
        /// <summary>
        /// 数据编码组件
        /// </summary>
        public IEncoderComparerChannel EncoderComparer { get; protected set; }
        public virtual IBootstrap BindAsync() => this;
        public virtual IBootstrap CloseAsync() => this;
        public virtual IBootstrap ConnectdAsync() => this;
        public virtual IBootstrap SetLocalAdders(IPEndPoint endPoint) => this;
        public virtual IBootstrap SetRemoteAdders(IPEndPoint endPoint) => this;
        public virtual IBootstrap SetTimeOut(uint time) => this;
        public virtual IBootstrap SetChannelHandle<T>() where T : class, IChannelHandle, new() => this;
        public virtual IBootstrap SetDecoderChannel<T>() where T : class, IDecoderComparserChannel, new() => this;
        public virtual IBootstrap SetEncoderChannel<T>() where T : class, IEncoderComparerChannel, new() => this;
        public virtual IBootstrap Write(IMemory memory) => this;
        public virtual IBootstrap Flush() => this;
        public virtual IBootstrap WriteAndFlush(IMemory memory) => this;
        public virtual IBootstrap Write(IntPtr ssid, IMemory memory) => this;
        public virtual IBootstrap Flush(IntPtr ssid) => this;
        public virtual IBootstrap WriteAndFlush(IntPtr ssid, IMemory memory) => this;
    }
}
