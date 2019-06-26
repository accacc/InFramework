using System;
using System.Text.RegularExpressions;

namespace Derin.Core.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key, Func<T> getItemCallback) where T : class;
        bool Contains(string key);
        void Set(string key, object data, int cacheTime);
        void Invalidate(string key);
        void Invalidate(Regex pattern);
        void Clear();
        bool IsSet(string key);
    }
}
