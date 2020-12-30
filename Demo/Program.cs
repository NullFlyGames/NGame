using System;
using System.Threading.Tasks;
using System.Net;
using NGame.RPC;

namespace Demo
{
    class Program
    {
        public class DispatcherHandle : AbstractChannelHandle
        {
            public override void ChannelConnectdCompleted(IChannel context)
            {
                Ex.Log("");
            }
            public override void ChannelActive(IChannel context)
            {
                Ex.Log($"连接成功：{context.RemoteAddress.ToString()}");
            }
        }
       
        static void Main(string[] args)
        {
            Bootstrap bootstrap1 = new Bootstrap();
            bootstrap1.SetTimeOut(10000);
            bootstrap1.SetChannelHandle<DispatcherHandle>();
            bootstrap1.SetChannel<SocketChannelContext>();
            bootstrap1.SetDecoderChannel<DefaultDecoderComparserChannel>();
            bootstrap1.SetEncoderChannel<DefaultEncoderComparserChannel>();
            bootstrap1.SetRemoteAdders(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            bootstrap1.ConnectdAsync();
            NCore.Initlizition();
            //NCore.LoadSystem<TestSystem>();

            //TestEntity entity = NCore.Create<TestEntity>();
            //entity.AddComponent<TestComponent>();

            while (true)
            {
                NCore.FixedUpdate();
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
