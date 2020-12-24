using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    internal class ReferencePool
    {
        public T New<T>() where T : class, IEntity, new()
        {
            return null;
        }

        public void Recycle(IEntity reference)
        {

        }
    }
}