using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ReferencePool
{
    public T New<T>() where T : class, IReference, new()
    {
        return null;
    }

    public void Recycle(IReference reference)
    {

    }
}
