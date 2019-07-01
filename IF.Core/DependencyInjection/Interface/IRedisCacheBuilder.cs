namespace IF.Core.DependencyInjection
{
    public interface IRedisCacheBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder AddJsonSerializer();
    }
}
