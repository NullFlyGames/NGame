using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Web
{
    public class WebManaged : Managed.AbstractManaged
    {
        private List<DownloadFileWebChannel> channels = new List<DownloadFileWebChannel>();
        public DownloadFileWebChannel Download()
        {
            DownloadFileWebChannel web = new DownloadFileWebChannel();
            channels.Add(web);
            return web;
        }
        public void RemoveWenChannel(DownloadFileWebChannel channel)
        {
            channel.Dispose();
            channels.Remove(channel);
        }
        public override void Update(float time)
        {
            
        }
    }
}
