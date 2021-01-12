using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Resource
{
    public abstract class AbstractAssetChannel : IAsset
    {
        public abstract string Name { get; }
        public abstract object Res { get; }
    }
}
