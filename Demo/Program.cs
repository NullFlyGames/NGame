using System;
using System.Threading.Tasks;
using System.Net;
using NGame.RPC;
using System.Collections.Generic;

namespace Demo
{
    class Program
    {
        public class DispatcherHandle : AbstractChannelHandle
        {
            public override void ChannelConnectdCompleted(ISocketChannelContext context)
            {
                Ex.Log("");
            }
            public override void ChannelActive(ISocketChannelContext context)
            {
                Ex.Log($"连接成功：{context.RemoteAddress.ToString()}");
                Memory memory = Memory.GetMemory();
                memory.Write("我这里是客户端");
                context.WriteAndFlush(memory);
            }
            public override async void ChannelReadComplete(ISocketChannelContext context, IMemory message)
            {
                Ex.Log($"收到数据：{context.RemoteAddress.ToString()} {context.SSID} {message.ToString()}");
                await Task.Delay(10);
                Memory memory = Memory.GetMemory();
                memory.Write("我这里是客户端");
                context.WriteAndFlush(memory);
                message.Recycle();
            }
        }

        static float times = 0;
        static void Main(string[] args)
        {
            OpenSocket(1000);
            NCore.Initlizition();
            while (true)
            {
                NCore.FixedUpdate(times);
                System.Threading.Thread.Sleep(10);
                times += 0.001f;
            }
        }

        static async void OpenSocket(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Bootstrap bootstrap = new Bootstrap();
                bootstrap.SetTimeOut(10000);
                bootstrap.SetChannelHandle<DispatcherHandle>();
                bootstrap.SetDecoderChannel<DefaultDecoderComparserChannel>();
                bootstrap.SetEncoderChannel<DefaultEncoderComparserChannel>();
                bootstrap.SetRemoteAdders(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                bootstrap.ConnectdAsync();
                await Task.Delay(10);
            }
        }
    }
}