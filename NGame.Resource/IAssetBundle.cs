using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Resource
{
    public interface IAssetBundle
    {
        string Name { get; }
        int RefCount { get; }
        void Install(string path);
        T Load<T>(string name);
        void UnInstall();
    }
}
