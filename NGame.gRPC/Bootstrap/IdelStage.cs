using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.RPC
{
    public enum IdelState
    {
        Write,
        Read,
        WriteAndRead,
    }
    public class IdelStage
    {
        class Stage
        {
            public float ChangeTime;
            public ISocketChannelContext ChannelContext;
        }
        readonly IdelState State;
        readonly float CheckTime;
        readonly IHandleChannel Handle;
        readonly ConcurrentDictionary<int, Stage> _Stage;

        public IdelStage(IdelState state, IHandleChannel handle, float time)
        {
            State = state;
            CheckTime = time;
            Handle = handle;
            _Stage = new ConcurrentDictionary<int, Stage>();
        }
        internal void Update(float time)
        {
            foreach (var item in _Stage)
            {
                if (NCore.Timer - item.Value.ChangeTime > CheckTime)
                {
                    Handle.OnTiggerEventHandle(item.Value.ChannelContext);
                    item.Value.ChangeTime = time;
                }
            }
        }

        internal void OnConnect(ISocketChannelContext context)
        {
            Stage stage = new Stage() { ChannelContext = context, ChangeTime = NCore.Timer };
            if (_Stage.TryAdd(context.id, stage))
            {
                TcpClientChannel channel = (TcpClientChannel)context;
                channel.OnClose += OnDisconnectd;
                channel.OnError += OnError;
                channel.OnRecvie += OnRecvie;
                channel.OnSend += OnSend;
                Handle.OnConnectHandle(context);
                return;
            }
            Ex.Log("已存在的连接");
        }
        internal void OnDisconnectd(ISocketChannelContext context)
        {
            if (_Stage.TryGetValue(context.id, out Stage stage))
            {
                Handle.OnDisconnectdHandle(context);
                _Stage.TryRemove(context.id, out stage);
                return;
            }
            Ex.Log("未找到连接数据");
        }

        internal void OnError(ISocketChannelContext context, Exception ex)
        {
            if (_Stage.TryGetValue(context.id, out Stage stage))
            {
                Handle.OnErrorHandle(context, ex);
                return;
            }
            Ex.Log("未找到连接数据");
        }
        internal void OnRecvie(ISocketChannelContext context, IMemory memory)
        {
            if (_Stage.TryGetValue(context.id, out Stage stage))
            {
                if (State == IdelState.Read || State == IdelState.WriteAndRead)
                {
                    stage.ChangeTime = NCore.Timer;
                }
                Handle.OnReadCompletedHandle(context, memory);
                return;
            }
            Ex.Log("未找到连接数据");
        }
        internal void OnSend(ISocketChannelContext context, IMemory memory)
        {
            if (_Stage.TryGetValue(context.id, out Stage stage))
            {
                if (State == IdelState.Write || State == IdelState.WriteAndRead)
                {
                    stage.ChangeTime = NCore.Timer;
                }
                Handle.OnWriteCompletedHandle(context, memory);
                return;
            }
            Ex.Log("未找到连接数据");
        }
        internal void OnClose()
        {
            Handle.OnDispose();
        }
    }
}
