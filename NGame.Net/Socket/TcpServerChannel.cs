using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace NGame.Net
{
    class TcpServerChannel : AbstractSocketChannel
    {
        Action _OnBind;
        Action _OnClose;
        Action<ITcpSocket> _OnAccept;
        Action<ITcpSocket, Exception> _OnError;
        //Action<ITcpSocket, IMemory> _OnRecvie;
        //Action<ITcpSocket, IMemory> _OnSend;
        //Action<ITcpSocket> _OnDisconnect;



        public event Action<ITcpSocket> OnAccept
        {
            add { _OnAccept += value; }
            remove { _OnAccept -= value; }
        }

        public event Action OnBind
        {
            add { _OnBind += value; }
            remove { _OnBind -= value; }
        }

        public event Action OnClose
        {
            add { _OnClose += value; }
            remove { _OnClose -= value; }
        }
        public event Action<ITcpSocket, Exception> OnError
        {
            add { _OnError += value; }
            remove { _OnError -= value; }
        }
        //public event Action<ITcpSocket, IMemory> OnRecvie
        //{
        //    add { _OnRecvie += value; }
        //    remove { _OnRecvie -= value; }
        //}
        //public event Action<ITcpSocket, IMemory> OnSend
        //{
        //    add { _OnSend += value; }
        //    remove { _OnSend -= value; }
        //}
        //public event Action<ITcpSocket> OnDisconnect
        //{
        //    add { _OnDisconnect += value; }
        //    remove { _OnDisconnect -= value; }
        //}
        /// <summary>
        /// 绑定本地地址并监听网络连接
        /// </summary>
        /// <param name="adder"></param>
        public override void DoBind(IPEndPoint adder)
        {
            try
            {
                _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _sock.Bind(adder);
                _sock.Listen(Environment.ProcessorCount * 2);
                AbstractSocketAsyncEventArgs args = new AbstractSocketAsyncEventArgs(this);
                ThreadPool.QueueUserWorkItem(DoAccept, args);
                _OnBind?.Invoke();
            }
            catch (Exception ex)
            {
                _OnError?.Invoke(this, ex);
            }
        }
        public override void DoAccept(object o)
        {
            bool flag = false;
            try
            {
                var args = (AbstractSocketAsyncEventArgs)o;
                args.AcceptSocket = null;
                flag = _sock.AcceptAsync(args);
                if (flag == true) return;
                if (_OnError != null) _OnError(this, new SocketException((int)args.SocketError));
            }
            catch (Exception ex)
            {
                Ex.Log(ex.Message);
                _OnError?.Invoke(this, ex);
            }
        }
        public override void OnAcceptCompleted(AbstractSocketAsyncEventArgs args)
        {
            if (args.SocketError != SocketError.Success)
            {
                _OnError?.Invoke(this, new SocketException((int)args.SocketError));
                return;
            }
            TcpClientChannel channel = new TcpClientChannel(args.AcceptSocket);
            //channel.OnClose += ChannelClose;
            //channel.OnRecvie += ChannelRecvieCompleted;
            //channel.OnSend += ChannelSendCompleted;
            //channel.OnError += ChannelError;
            ThreadPool.QueueUserWorkItem(channel.DoRecvie);
            ThreadPool.QueueUserWorkItem(ChannelConnectd, channel);
            ThreadPool.QueueUserWorkItem(DoAccept, args);
        }
        //void ChannelClose(TcpClientChannel channel)
        //{
        //    if (_OnDisconnect == null)
        //        return;
        //    _OnDisconnect(channel);
        //}
        void ChannelConnectd(object o)
        {
            if (_OnAccept == null)
                return;
            _OnAccept((TcpClientChannel)o);
        }
        //void ChannelSendCompleted(TcpClientChannel channel, IMemory memory)
        //{
        //    if (_OnSend == null)
        //        return;
        //    _OnSend(channel, memory);
        //}
        //void ChannelRecvieCompleted(TcpClientChannel channel, IMemory memory)
        //{
        //    if (_OnRecvie == null)
        //        return;
        //    _OnRecvie(channel, memory);
        //}
        //void ChannelError(TcpClientChannel channel, Exception exception)
        //{
        //    if (_OnError == null)
        //        return;
        //    _OnError(channel, exception);
        //}
        public override void Dispose()
        {
            if (_OnClose != null) _OnClose();
            try { _sock.Shutdown(SocketShutdown.Both); } catch { }
            try { _sock.Close(); } catch { }
        }
    }
}
