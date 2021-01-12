using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.UIForm
{
    public class Varable<TKey, TVal>
    {
        TKey _key;
        Action<TKey, TVal> _change;
        TVal _val;
        public TVal Val
        {
            get
            {
                return _val;
            }
            set
            {
                _val = value;
                _change(_key, _val);
            }
        }

        public Varable(TKey key, TVal val, Action<TKey, TVal> change)
        {
            _key = key;
            Val = val;
            _change = change;
        }
    }
}
