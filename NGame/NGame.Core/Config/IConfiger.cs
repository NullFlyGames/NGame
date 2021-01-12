using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Config
{
    public interface IConfiger
    {
        T GetConfig<T>(string key);
        void SetConfig<T>(string key, T val);
        void Save();
        void Load(string name);
    }
}
