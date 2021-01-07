using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Managed
{
    public interface IManaged : IDisposable
    {
        void Initlizition();
        void Update(float time);
    }
}
