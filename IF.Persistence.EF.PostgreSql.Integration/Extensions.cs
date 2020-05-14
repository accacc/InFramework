using IF.Core.DependencyInjection.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IF.Persistence.EF.PostgreSql.Integration
{
    public static class Extension
    {
        public static IInFrameworkBuilder AddDbContext<T>(this IInFrameworkBuilder @if, IServiceCollection services, string connectionString, string workingAssembly) where T : DbContext
        {
            
            services.AddDbContext<T>(options =>
            {
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(workingAssembly));
            }, ServiceLifetime.Transient);

            services.AddTransient<Core.Persistence.IRepository>(provider => new GenericRepository(provider.GetService<T>()));


            return @if;

        }


    }
}

