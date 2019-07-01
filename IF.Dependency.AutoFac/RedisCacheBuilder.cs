//using IF.Core.Cache;
//using IF.Core.DependencyInjection;
//using IF.Core.Serialization;
//using IF.Redis;

//namespace IF.Dependency.AutoFac
//{


//    //TODO:Caglar bunu Redis projesine al.

//    public class RedisCacheBuilder : IRedisCacheBuilder
//    {

//        public IInFrameworkBuilder Builder { get; }
//        public RedisCacheBuilder(IInFrameworkBuilder dependencyInjection)
//        {
//            this.Builder = dependencyInjection;
//            this.Builder.RegisterType<RedisCacheService, ICacheService>(DependencyScope.PerScope);
//        }


//        public IInFrameworkBuilder AddJsonSerializer()
//        {
//            this.Builder.RegisterType<RedisJsonSerializer, IRedisSerializer>(DependencyScope.PerScope);
//            return this.Builder;
//        }



//    }
//}
