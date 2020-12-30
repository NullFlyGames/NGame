using NGame.Core;
using NGame.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class TestMatche : IMatcher
    {

        public bool Matche(IEntity entity)
        {
            return entity.GetComponent<TestComponent>() != null;
        }
    }
}
