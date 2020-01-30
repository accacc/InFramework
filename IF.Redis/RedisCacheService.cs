using IF.Core.Cache;
using IF.Core.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IF.Redis
{
    public class RedisCacheService : ICacheService
    {

        private readonly IDistributedCache distributedCache;
        private readonly IRedisSerializer serializer;
        public RedisCacheService(IDistributedCache distributedCache, IRedisSerializer serializer)
        {
            this.distributedCache = distributedCache;
            this.serializer = serializer;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }


        public T Get<T>(string key, Func<T> getItemCallback, int cacheTime = 100) where T : class
        {

            var data = this.Get<T>(key,cacheTime);

            if (!(data is CachedNullValue))
            {

                if (data == null)
                {
                    data = getItemCallback();

                    if (data == null)
                    {
                        this.distributedCache.Set(key, serializer.Serialize(CachedNullValue.Value));
                    }
                    else
                    {
                        this.distributedCache.Set(key, serializer.Serialize(data));
                    }
                }

            }

            return data;
        }

        private T Get<T>(string key, int cacheTime = 100)
        {
            var json = this.distributedCache.Get(key);

            if (json != null)
            {
                return serializer.Deserialize<T>(json);
            }

            else
            {
                return default(T);
            }
        }



        public void Invalidate(string key)
        {
            throw new NotImplementedException();
        }

        public void Invalidate(Regex pattern)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object data, int cacheTime)
        {
            this.distributedCache.Set(key, serializer.Serialize(data));
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> getItemCallback, int cacheTime=100) where T : class
        {
            var data = this.Get<T>(key);

            if (!(data is CachedNullValue))
            {

                if (data == null)
                {
                    data = await getItemCallback();

                    if (data == null)
                    {
                        this.distributedCache.Set(key, serializer.Serialize(CachedNullValue.Value));
                    }
                    else
                    {
                        this.distributedCache.Set(key, serializer.Serialize(data));
                    }
                }

            }

            return data;
        }
    }
}
