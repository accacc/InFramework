using Autofac;
using IF.Configuration;
using IF.Core.DependencyInjection;
using IF.Cqrs.Builders;
using IF.Dependency.AutoFac;
using IF.EventBus.RabbitMQ.Integration;
using IF.Swagger.Integration;
using IF.Template.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace IF.Template.Api
{
    public class Startup
    {

        IInFrameworkBuilder @if;
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            @if = new InFrameworkBuilder();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var settings =this.Configuration.GetSettings<IFTemplateSettings>();

            var domain = Assembly.Load("IF.Template.Domain");

            @if.AddSwagger(services, "v1", "InFramework Template Api", true)
               .AddApplicationSettings<IIFTemplateSettings>(settings)
                   .AddCqrs(cqrs =>
                   {

                       cqrs.AddHandler(

                           handler =>
                           {
                               handler.AddQueryHandlers()
                               .Decoration(d => d
                                     .AddPerformanceCounter()
                                     //.AddSimulatation()
                                     .AddErrorLogging()
                                     .AddAuditing()
                                     //.AddIdentity()
                                     )
                               .Build(new Assembly[] { domain });


                               handler.AddQueryAsyncHandlers()
                                   .Decoration(d => d
                                       //.AddSearching()
                                       .AddPerformanceCounter()
                                       //.AddSimulatation()

                                       .AddErrorLogging()
                                       .AddAuditing()
                                   //.AddIdentity()
                                   )
                                   .Build(new Assembly[] { domain });



                               handler.AddCommandHandlers()
                                     .Decoration(d => d
                                         .AddPerformanceCounter()
                                         .AddErrorLogging()
                                         .AddDataAnnonationValidation()
                                     //.AddAuditing()
                                     //.AddIdentity()
                                     )
                                    .Build(new Assembly[] { domain });


                               handler.AddCommandAsyncHandlers()
                                      .Decoration(d => d
                                       .AddPerformanceCounter()
                                       .AddErrorLogging()
                                       .AddDataAnnonationValidation()
                                   //.AddAuditing()
                                   //.AddIdentity()
                                   )
                                   .Build(new Assembly[] { domain });
                           });
                   })
                    .AddEventBus(bus =>
                    {
                        bus.AddRabbitMQEventBus(services, settings.RabbitMQConnection, "if_template").Build(new Assembly[] { domain });

                    })

                ;

            return services.Build(@if);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            @if.UseSwagger(app, "v1", "InFramework Template Api");
        }
    }
}
