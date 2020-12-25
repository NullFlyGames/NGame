using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    public interface IContext
    {
        void Initlizition();
        List<IEntity> GetGroup(IMatcher matcher);
    }
}
