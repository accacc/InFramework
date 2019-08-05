using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using IF.Core.EventBus;
using System.Reflection;

namespace IF.Core.DependencyInjection
{
    public class EventBusBuilder: IEventBusLogBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public EventBusBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build(Assembly[] assemblies)
        {
            this.Builder.RegisterClosedType(assemblies, typeof(IIFEventHandler<>), DependencyScope.PerRequest);
            return this.Builder;
        }
    }
}
