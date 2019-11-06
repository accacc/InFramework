using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;

namespace IF.Swagger.Integration
{
    public static class Extension
    {
        public static IInFrameworkBuilder AddSwagger(this IInFrameworkBuilder @if, IServiceCollection services, string version, string apiName, bool addTokenIntegration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = apiName + " Api v1", Version = version });


                if (addTokenIntegration)
                {

                    var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                    c.AddSecurityRequirement(security);

                }

            });


            return @if;

        }
        public static IInFrameworkBuilder UseSwagger(this IInFrameworkBuilder @if, IApplicationBuilder app, string version, string apiName)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/"+ version +"/swagger.json", apiName + " " + version));

            return @if;
        }

    }


}