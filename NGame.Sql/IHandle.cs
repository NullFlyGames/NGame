using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Sql
{
    public partial class DBManaged
    {
        public interface IHandle 
        {
            void Release();
        }
    }
}
