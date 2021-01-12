using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Resource
{
    public abstract class AbstractAssetBundleChannel : IAssetBundle
    {
        public abstract string Name { get; }
        public abstract int RefCount { get; }

        public abstract void Install(string path);
        public abstract T Load<T>(string name);
        public abstract void UnInstall();
    }
}
