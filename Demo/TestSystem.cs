using NGame.NCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class TestSystem : ExecuteSystem<TestMatche>
    {
        public TestMatche Mathces => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            var entitys = NCore.GetGroup(Mathces);
        }

     

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
