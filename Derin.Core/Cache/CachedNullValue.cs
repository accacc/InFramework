using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Cache
{
    public class CachedNullValue
    {
        public CachedNullValue()
        {

        }

        public volatile static CachedNullValue Value = new CachedNullValue();

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(CachedNullValue obj1, CachedNullValue obj2)
        {
            return true;
        }

        public static bool operator !=(CachedNullValue obj1, CachedNullValue obj2)
        {
            return false;
        }
    }
}
