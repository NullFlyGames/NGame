using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.NCore
{
    public class Context : IContext
    {
        private Dictionary<IMatcher, List<IEntity>> _groups = new Dictionary<IMatcher, List<IEntity>>();


        public List<IEntity> GetGroup(IMatcher matcher)
        {
            if (!_groups.TryGetValue(matcher, out List<IEntity> group))
            {

            }
            return group;
        }
        
    }
}
