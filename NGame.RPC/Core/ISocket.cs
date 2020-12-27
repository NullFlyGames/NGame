namespace NGame.RPC
{
    using NGame.Core;
    public interface ISocket
    {
        int id { get; }
        string Adders { get; }
        ushort Port { get; }
    }
}
