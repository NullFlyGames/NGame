using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Event
{

    public interface IEventArgs
    {
        string name { get; }
        object[] data { get; }
    }
}
