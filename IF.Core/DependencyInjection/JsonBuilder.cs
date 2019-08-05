using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using IF.Core.Json;

namespace IF.Core.DependencyInjection
{
    public class JsonBuilder:IJsonBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public JsonBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build()
        {

            this.Builder.RegisterType<NewtonsoftJsonSerializer, IJsonSerializer>(DependencyScope.PerScope);

            return this.Builder;
        }
    }
}
