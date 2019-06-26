using IF.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Redis
{
    public static class RedisBuilderExtension
    {
        public static IRedisCacheBuilder AddRedis<T>(this IRedisCacheBuilder redis)
        {

            return redis;
        }
    }
}
