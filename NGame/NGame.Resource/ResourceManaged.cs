using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGame.Managed;

namespace NGame.Resource
{
    public class ResourceManaged : AbstractManaged
    {
        private Dictionary<string, IAssetBundle> _bundles = new Dictionary<string, IAssetBundle>();
        public override void Update(float time)
        {

        }
        public T LoadAssetBundle<T>(string name, string path) where T : class, IAssetBundle, new()
        {
            if (_bundles.ContainsKey(name) == true) return (T)_bundles[name];

            T bundle = new T();
            bundle.Install(path);
            _bundles.Add(name, bundle);
            return bundle;
        }
        public T LoadObject<T>(string bundle, string name)
        {
            if (_bundles.ContainsKey(bundle) == false) return default;

            return _bundles[bundle].Load<T>(name);
        }
        public void UnLoadBundle(string name)
        {
            if (_bundles.ContainsKey(name) == false) return;

            _bundles[name].UnInstall();
            _bundles.Remove(name);
        }
    }
}
