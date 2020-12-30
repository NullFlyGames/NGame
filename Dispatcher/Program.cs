using System;
using System.Net;
using System.Threading.Tasks;
using NGame.RPC;

namespace Dispatcher
{

    public class DispatcherHandle : AbstractChannelHandle
    {
        public override void ChannelConnectdCompleted(IChannel context)
        {
            Ex.Log($"有新连接进入：{context.RemoteAddress.ToString()} {context.SSID} {context.GUID}");
        }
    }
    class Program
    {
        static void Main(string[] args) 
        {
            Console.Title = "分发器";

            Bootstrap bootstrap = new Bootstrap();
            bootstrap.SetTimeOut(10000);
            bootstrap.SetChannelHandle<DispatcherHandle>();
            bootstrap.SetChannel<SocketChannelContext>();
            bootstrap.SetDecoderChannel<DefaultDecoderComparserChannel>();
            bootstrap.SetEncoderChannel<DefaultEncoderComparserChannel>();
            bootstrap.SetLocalAdders(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            bootstrap.BindAsync();
        }
    }
}
