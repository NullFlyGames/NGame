using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    public interface IMatcher
    {
        List<IEntity> AllOf(params Type[] InterestComponent);
    }
}
