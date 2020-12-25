using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            NCore.LoadSystem<TestSystem>();

            TestEntity entity = NCore.Create<TestEntity>();
            entity.AddComponent<TestComponent>();
        }
    }
}
