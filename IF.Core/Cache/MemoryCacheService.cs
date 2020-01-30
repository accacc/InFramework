using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Cache
{
    public class MemoryCacheService : ICacheService
    {

        private ObjectCache Cache { get { return MemoryCache.Default; } }

        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);


        public MemoryCacheService()
        {
        }

        public T Get<T>(string key, Func<T> getItemCallback, int cacheTime = 100) where T : class
        {
            T data = default(T);

            var cached = Cache.Get(key);

            if (!(cached is CachedNullValue))
            {
                data = cached as T;

                if (data == null)
                {
                    lock (Cache)
                    {

                        data = getItemCallback();

                        if (data == null)
                        {
                            Cache.Add(key, CachedNullValue.Value, GetPolicy(cacheTime));
                        }
                        else
                        {
                            Cache.Add(key, data, GetPolicy(100));
                        }

                    }
                }

            }

            return data;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> getItemCallback, int cacheTime=100) where T : class
        {
            T data = default(T);

            var cached = Cache.Get(key);

            if (!(cached is CachedNullValue))
            {
                data = cached as T;

                if (data == null)
                {
                    await semaphoreSlim.WaitAsync();


                    try
                    {
                        data = await getItemCallback();

                        if (data == null)
                        {
                            Cache.Add(key, CachedNullValue.Value, GetPolicy(cacheTime));
                        }
                        else
                        {
                            Cache.Add(key, data, GetPolicy(100));
                        }
                    }
                    finally
                    {
                        semaphoreSlim.Release();
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

        

        public string GetString<T>(string key, Func<T> getItemCallback) where T : class
        {

            throw new NotImplementedException();
            //var data = this.Get<T>(key, getItemCallback);

            // return this.jsonSerializer.Serialize(data);
        }
    }
}
