using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Managed
{
    public abstract class AbstractManaged : IManaged
    {
        public virtual void Dispose() { }
        public virtual void Initlizition() { }
        public virtual void Update(float time) { }
    }
}
