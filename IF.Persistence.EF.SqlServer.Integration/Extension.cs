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
        public static IInFrameworkBuilder AddDbContext<T>(this IInFrameworkBuilder @if, IServiceCollection services, string connectionString,string workingAssembly=null) where T : DbContext
        {

            if (workingAssembly != null)
            {
                services.AddDbContext<T>(options =>
                {
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(workingAssembly));
                }, ServiceLifetime.Transient);
            }
            else
            {
                services.AddDbContext<T>(options =>
                {
                    options.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient);
            }

            services.AddTransient<IRepository>(provider => new  GenericRepository(provider.GetService<T>()));


            return @if;

        }
      

    }
}
