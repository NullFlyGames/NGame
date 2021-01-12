using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Web
{
    public class WebManaged
    {
        public T Download<T>(string url, string local) where T :class, IWebChannel,new()
        {
            T web = new T();
            web.Url = url;
            web.LocalPath = local;
            web.Progres = 0;
            web.OnStart();
            return web;
        }
    }
}
