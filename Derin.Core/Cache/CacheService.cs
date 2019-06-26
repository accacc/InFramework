using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Derin.Core.Cache
{
    public class CacheService : ICacheService
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public T Get<T>(string key, Func<T> getItemCallback) where T : class
        {
            T data = default(T);

            var cached = Cache.Get(key);

            if (!(cached is CachedNullValue))
            {
                data = cached as T;

                if (data == null)
                {
                    data = getItemCallback();

                    if (data == null)
                    {
                        Cache.Add(key, CachedNullValue.Value, GetPolicy(100));
                    }
                    else
                    {
                        Cache.Add(key, data, GetPolicy(100));
                    }
                }

            }

            return data;
        }




        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object data, int cacheTime)
        {
            CacheItemPolicy policy = GetPolicy(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        private CacheItemPolicy GetPolicy(int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            return policy;
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }

        public void Invalidate(Regex pattern)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            var allKeys = Cache.Select(o => o.Key);
            Parallel.ForEach(allKeys, key => Cache.Remove(key));
        }

        public string GetServiceId()
        {
            throw new NotImplementedException();
        }
    }
}
