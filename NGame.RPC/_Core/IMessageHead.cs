using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core.RPC
{
    /// <summary>
    /// RPC包头
    /// </summary>
    public interface IMessageHead
    {
        int form { get; set; }
        int to { get; set; }
        int opCode { get; set; }
    }
}
