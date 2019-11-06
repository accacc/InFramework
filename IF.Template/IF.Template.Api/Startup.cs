using Autofac;
using IF.Configuration;
using IF.Core.DependencyInjection.Interface;
using IF.Cqrs.Builders;
using IF.Dependency.AutoFac;
using IF.EventBus.RabbitMQ.Integration;
using IF.Persistence;
using IF.Persistence.EF.Core;
using IF.Persistence.EF.SqlServer.Integration;
using IF.Swagger.Integration;
using IF.Template.Cqrs;
using IF.Template.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var settings = this.Configuration.GetSettings<IFTemplateAppSettings>();

            @if.AddDbContext<TestDbContext>(services, settings.Database.ConnectionString);

            var handlers = Assembly.Load("IF.Template.Cqrs");

            @if.AddSwagger(services, "v1", "InFramework Template Api", true)
                   .AddApplicationSettings<IIFTemplateAppSettings>(settings)
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
                               .Build(new Assembly[] { handlers });


                               handler.AddQueryAsyncHandlers()
                                   .Decoration(d => d
                                       //.AddSearching()
                                       .AddPerformanceCounter()
                                       //.AddSimulatation()

                                       .AddErrorLogging()
                                       .AddAuditing()
                                   //.AddIdentity()
                                   )
                                   .Build(new Assembly[] { handlers });



                               handler.AddCommandHandlers()
                                     .Decoration(d => d
                                         .AddPerformanceCounter()
                                         .AddErrorLogging()
                                         .AddDataAnnonationValidation()
                                     //.AddAuditing()
                                     //.AddIdentity()
                                     )
                                    .Build(new Assembly[] { handlers });


                               handler.AddCommandAsyncHandlers()
                                      .Decoration(d => d
                                       .AddPerformanceCounter()
                                       .AddErrorLogging()
                                       .AddDataAnnonationValidation()
                                   //.AddAuditing()
                                   //.AddIdentity()
                                   )
                                   .Build(new Assembly[] { handlers });
                           });
                   })
                    .AddEventBus(bus =>
                    {
                        bus.AddRabbitMQEventBus(services, settings.RabbitMQConnection, "if_template").Build(new Assembly[] { handlers });
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
