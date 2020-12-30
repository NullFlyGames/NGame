using System;
using System.Net;
using System.Threading.Tasks;
using NGame.RPC;

namespace Dispatcher
{

    public class DispatcherHandle : AbstractChannelHandle
    {
        public override void ChannelConnectdCompleted(ISocketChannelContext context)
        {
            Ex.Log($"有新连接进入：{context.RemoteAddress.ToString()} {context.SSID} {context.GUID}");
        }
        public override async void ChannelReadComplete(ISocketChannelContext context, IMemory message)
        {
            Ex.Log($"收到数据：{context.RemoteAddress.ToString()} {context.SSID}  {message.ToString()}");
            await Task.Delay(10);
            Memory memory = Memory.GetMemory();
            memory.Write("我这里是服务器");
            context.WriteAndFlush(memory);
            message.Recycle();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "分发器";
            ServerBootstap bootstap = new ServerBootstap();
            bootstap.SetTimeOut(10000);
            bootstap.SetChannelHandle<DispatcherHandle>();
            bootstap.SetDecoderChannel<DefaultDecoderComparserChannel>();
            bootstap.SetEncoderChannel<DefaultEncoderComparserChannel>();
            bootstap.SetLocalAdders(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            bootstap.BindAsync();
        }
    }
}
