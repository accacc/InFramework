using IF.Core.DependencyInjection.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IF.Core.Persistence;

namespace IF.Persistence.EF.SqlServer.Integration
{
    public static class Extension
    {
        public static IInFrameworkBuilder AddDbContext<T>(this IInFrameworkBuilder @if, IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);

            services.AddTransient<IRepository>(provider => new  GenericRepository(provider.GetService<T>()));


            return @if;

        }
      

    }
}
