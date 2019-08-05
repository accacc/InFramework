namespace IF.Core.DependencyInjection.Interface
{
    public interface IRedisCacheBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder AddJsonSerializer();
    }
}
