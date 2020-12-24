using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.NSocket.Session;
public sealed partial class NSocket
{
    public static T Connect<T>(string adder, ushort port) where T : NGame.NSocket.Message.Handle, new()
    {
        TcpSession session = NCore.New<TcpSession>();
        if (session.Connect(adder, port) == false)
        {
            NCore.Recycle(session);
            return null;
        }
        T handle = NCore.New<T>();
        handle.Awake();

        return handle;
    }
}
