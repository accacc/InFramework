using IF.Core.DependencyInjection;
using IF.Core.Log;

namespace IF.Dependency.AutoFac
{
    public class ApplicationLoggerBuilder: IApplicationLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public ApplicationLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        

        //public ILoggerBuilder AddLogger(string url,string db)
        //{           
        //    return this;
        //}

        public IInFrameworkBuilder AddNullLogger()
        {
            this.Builder.RegisterType<NullLog, ILogService>(DependencyScope.Single);

            return this.Builder;
        }

        //public IInFrameworkBuilder Build()
        //{
        //    return this.Builder;
        //}
    }
}
