using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Core
{
    interface IObjectPool
    {
        Type Owner { get; }
        void Clear();
    }
}
