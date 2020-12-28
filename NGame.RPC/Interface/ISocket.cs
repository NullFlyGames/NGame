using System.Net.Sockets;

namespace NGame.RPC
{
    interface ISocket<MessageHandle> where MessageHandle : IHandle
    {
        int id { get; }
        string Adders { get; }
        ushort Port { get; }
        MessageHandle Handles { get; }
        Socket Socket { get; }
    }
}
