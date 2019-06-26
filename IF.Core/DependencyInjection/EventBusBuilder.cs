using IF.Core.DependencyInjection;
using IF.Core.EventBus;
using System.Reflection;

namespace IF.Dependency.AutoFac
{
    public class EventBusBuilder: IEventBusBuilder
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
