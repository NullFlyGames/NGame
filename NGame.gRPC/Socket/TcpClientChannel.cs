using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace NGame.NRPC
{
    class TcpClientChannel : AbstractSocketChannel
    {

        Action<TcpClientChannel> _OnConnectd;
        Action<TcpClientChannel, IMemory> _OnSend;
        Action<TcpClientChannel, IMemory> _OnRecvie;
        Action<TcpClientChannel> _OnClose;
        Action<TcpClientChannel, Exception> _OnError;
        bool isStarSend = false;
        ConcurrentQueue<IMemory> _messages = new ConcurrentQueue<IMemory>();

        /// <summary>
        /// 连接完成事件
        /// </summary>
        public event Action<TcpClientChannel> OnConnectd
        {
            add { _OnConnectd += value; }
            remove { _OnConnectd -= value; }
        }
        /// <summary>
        /// 数据发送完成事件
        /// </summary>
        public event Action<TcpClientChannel, IMemory> OnSend
        {
            add { _OnSend += value; }
            remove { _OnSend -= value; }
        }
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event Action<TcpClientChannel, IMemory> OnRecvie
        {
            add { _OnRecvie += value; }
            remove { _OnRecvie -= value; }
        }
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event Action<TcpClientChannel> OnClose
        {
            add { _OnClose += value; }
            remove { _OnClose -= value; }
        }
        /// <summary>
        /// 错误事件
        /// </summary>
        public event Action<TcpClientChannel, Exception> OnError
        {
            add { _OnError += value; }
            remove { _OnError -= value; }
        }
        public TcpClientChannel(Socket socket)
        {
            _sock = socket;
        }
        public TcpClientChannel()
        {
        }
        /// <summary>
        /// 开始连接远程地址
        /// </summary>
        /// <param name="o"></param>
        public override void DoConnectd(IPEndPoint adder)
        {

            try
            {
                var args = new AbstractSocketAsyncEventArgs(this);
                _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                args.RemoteEndPoint = adder;

                if (_sock.ConnectAsync(args)) return;

                _OnError?.Invoke(this, new SocketException((int)args.SocketError));
                AbstractSocketAsyncEventArgs.Push(args);
            }
            catch
            {
                _OnError?.Invoke(this, new SocketException());
            }
        }

        public override void OnConnectdCompleted(AbstractSocketAsyncEventArgs args)
        {
            if (args.SocketError != SocketError.Success)
            {
                _OnError?.Invoke(this, new SocketException((int)args.SocketError));
                return;
            }
            _OnConnectd?.Invoke(this);
            ThreadPool.QueueUserWorkItem(DoRecvie);
            args.Dispose();
        }
        public override void DoRecvie(object o)
        {
            try
            {
                var args = AbstractSocketAsyncEventArgs.GetAbstractSocketAsync(this);

                if (_sock.ReceiveAsync(args)) return;

                _OnError?.Invoke(this, new SocketException((int)args.SocketError));
            }
            catch (Exception ex)
            {
                _OnError?.Invoke(this, new SocketException());
                Ex.Log("接收数据失败:"+ ex);
            }
        }
        public override void OnRecvieCompleted(AbstractSocketAsyncEventArgs args)
        {
            if (args.SocketError != SocketError.Success)
            {
                Dispose();
                return;
            }
            if (args.BytesTransferred > 0)
            {
                args._Memory.Offset = args.BytesTransferred;
                _OnRecvie?.Invoke(this, args._Memory);
            }
            AbstractSocketAsyncEventArgs.Push(args);
            ThreadPool.QueueUserWorkItem(DoRecvie);
        }

        public override void DoSend(object o)
        {
            try
            {
                if (isStarSend || _messages.Count <= 0)
                {
                    return;
                }
                isStarSend = true;
                while (_messages.TryDequeue(out IMemory memory))
                {
                    if (memory == null) continue;
                    var args = AbstractSocketAsyncEventArgs.GetAbstractSocketAsync(this, memory);
                    if (_sock.SendAsync(args) == false)
                    {
                        _OnError?.Invoke(this, new SocketException((int)args.SocketError));
                        AbstractSocketAsyncEventArgs.Push(args);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (_OnError != null) _OnError(this, new SocketException());
            }
        }
        public override void OnSendCompleted(AbstractSocketAsyncEventArgs args)
        {
            if (args.SocketError != SocketError.Success)
            {
                Dispose();
                return;
            }
            isStarSend = false;
            _OnSend?.Invoke(this, args._Memory);
            AbstractSocketAsyncEventArgs.Push(args);
        }
        public override void Dispose()
        {
            _OnClose?.Invoke(this);
            try { _sock.Shutdown(SocketShutdown.Both); } catch { }
            try { _sock.Close(); } catch { }
        }
        public override void Flush()
        {
            ThreadPool.QueueUserWorkItem(DoSend);
        }
        public override void Write(IMemory memory)
        {
            _messages.Enqueue(memory);
        }
        public override void WriteAndFlush(IMemory memory)
        {
            Write(memory);
            Flush();
        }
    }
}
