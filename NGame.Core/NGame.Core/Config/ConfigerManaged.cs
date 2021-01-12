using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Config
{
    public class ConfigerManaged : Managed.AbstractManaged
    {
        readonly Dictionary<string, IConfiger> _configs = new Dictionary<string, IConfiger>();

        public T LoadConfig<T>(string cfgName) where T : class, IConfiger, new()
        {
            if (_configs.ContainsKey(cfgName) == false) return (T)_configs[cfgName];

            T cfg = new T();
            cfg.Load(cfgName);
            _configs.Add(cfgName, cfg);
            return cfg;
        }
        public T GetConfigChannel<T>(string cfgName) where T : IConfiger
        {
            if (_configs.ContainsKey(cfgName) == false) return default;

            return (T)_configs[cfgName];
        }

        public void UnLoadConfig<T>(string cfgName) where T : IConfiger
        {
            if (_configs.ContainsKey(cfgName) == false) return;

            _configs[cfgName].Save();

            _configs.Remove(cfgName);
        }
    }
}
