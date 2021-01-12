using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Systems
{
    public interface IExecuteSystem : IDisposable
    {
        void Execute(float time);
        void Initlizition();
    }
}
