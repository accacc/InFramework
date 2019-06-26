using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Mvc.Cookie
{
    public interface ICookieContainer
    {
        bool Exists(string key);

        string GetValue(string key);

        T GetValue<T>(string key);

        void SetValue(string key, object value, DateTime expires);
    }
}
