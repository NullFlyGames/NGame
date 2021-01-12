using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Resource
{
    public interface IAsset
    {
        string Name { get; }
        object Res { get; }
    }
}
