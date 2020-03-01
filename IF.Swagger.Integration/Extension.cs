using IF.Core.DependencyInjection.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.Swagger;
using Microsoft.OpenApi.Models;

namespace IF.Swagger.Integration
{
    public static class Extension
    {
        public static IInFrameworkBuilder AddSwagger(this IInFrameworkBuilder @if, IServiceCollection services, string version, string apiName, bool addTokenIntegration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName + " Api v1", Version = version });

                if (addTokenIntegration)
                {

                    var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });

                }

            });


            return @if;

        }
        public static IInFrameworkBuilder UseSwagger(this IInFrameworkBuilder @if, IApplicationBuilder app, string version, string apiName)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/" + version + "/swagger.json", apiName + " " + version));

            return @if;
        }

    }


}