using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Entity
{
    public class CreateEntityEventArgs : Event.IEventArgs
    {
        public string name => "CreateEntityCallback";

        public object[] data
        {
            get;
            private set;
        }

        public CreateEntityEventArgs(params object[] parmas)
        {
            this.data = parmas;
        }
    }
}
