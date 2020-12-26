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
        readonly TestMatche _matche = new TestMatche();
        public TestMatche Mathces => _matche;

        public void Dispose()
        {
            
        }

        public void Execute()
        {
            var entitys = Context.GetGroup(Mathces);
            if (entitys.Count <= 0)
            {
                Ex.Log("not entity");
                return; 
            }
            Ex.Log("Execute");
        }

     

        public void Initialize()
        {
            Ex.Log("初始化系统");
        }
    }
}
