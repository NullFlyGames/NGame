using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Config
{
    public abstract class AbstractConfigChannel : IConfiger
    {
        public abstract T GetConfig<T>(string key);
        public abstract void Load(string name);
        public abstract void Save();
        public abstract void SetConfig<T>(string key, T val);
    }
}
