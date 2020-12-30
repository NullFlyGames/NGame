using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    /// <summary>
    /// 网络消息处理
    /// </summary>
    public interface IChannelHandle
    {
        /// <summary>
        /// 连接成功，可以发送消息
        /// </summary>
        /// <param name="context"></param>
        void ChannelActive(ISocketChannelContext context);
        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="context"></param>
        void ChannelInactive(ISocketChannelContext context);
        /// <summary>
        /// 消息接收完毕
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        void ChannelReadComplete(ISocketChannelContext context, IMemory message);
        /// <summary>
        /// 异常消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        void ExceptionCaught(ISocketChannelContext context, Exception exception);
        /// <summary>
        /// 消息写入完毕
        /// </summary>
        /// <param name="channelHandleContext"></param>
        /// <param name="momery"></param>
        void ChannelWriterCompleted(ISocketChannelContext channelHandleContext);
        /// <summary>
        /// 会话绑定成功
        /// </summary>
        /// <param name="context"></param>
        void ChannelBindingCompleted(ISocketChannelContext context);
        /// <summary>
        /// 会话连接成功
        /// </summary>
        /// <param name="context"></param>
        void ChannelConnectdCompleted(ISocketChannelContext context);

        /// <summary>
        /// 会话连接成功
        /// </summary>
        /// <param name="context"></param>
        void ChannelDisconnectdCompleted(ISocketChannelContext context);
    }
}
