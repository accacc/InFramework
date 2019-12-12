using IF.Configuration;
using IF.Core.DependencyInjection.Interface;
using IF.Cqrs.Builders;
using IF.Dependency.AutoFac;
using IF.MongoDB.Integration;
using IF.Persistence;
using IF.Persistence.EF.SqlServer.Integration;
using IF.Template.Persistence.EF;
using IF.Template.Scheduler.Infrastracture;
using IF.Template.Scheduler.Scheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel;
using System.Reflection;

namespace IF.Template.Scheduler
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var settings = this.Configuration.GetSettings<SchedulerAppSettings>();

            IInFrameworkBuilder @if = new InFrameworkBuilder();

            @if.AddDbContext<TestDbContext>(services, settings.Database.ConnectionString);

            var domain = Assembly.Load("IF.Template.Cqrs");
            var repository = Assembly.Load("IF.Template.Persistence.EF");

            @if
            .AddApplicationSettings<ISchedulerAppSettings>(settings)
          //.AddNewstonJson(json => json.Build())
          //.AddRedisCache(redis => redis.AddJsonSerializer())
          .AddMongo(mongo => mongo.AddMongoSingleConnectionStrategy(settings.MongoConnection, c =>
          {
              //c.AddAuditLogger();
              c.AddApplicationLogger();
              //c.AddEventBusLogger();
              //c.AddNotificationLogger();
              //c.AddSmsLogger();
              //c.AddEmailLogger();
              //c.AddPerformanceLogger();
          }
          ))
         
          .AddCqrs(cqrs =>
          {

              cqrs.AddHandler(

                   handler =>
                       {                        

                           handler.AddQueryAsyncHandlers()
                               //.Decoration(d => d
                               //    //.AddSearching()
                               //    .AddPerformanceCounter()
                               //    //.AddSimulatation()

                               //    .AddErrorLogging()
                               //    .AddAuditing()
                               ////.AddIdentity()
                               //)
                               .Build(new Assembly[] { domain });

                           handler.AddCommandAsyncHandlers()
                               //   .Decoration(d => d
                               //    .AddPerformanceCounter()
                               //    .AddErrorLogging()
                               //    .AddDataAnnonationValidation()
                               ////.AddAuditing()
                               ////.AddIdentity()
                               //)
                               .Build(new Assembly[] { domain });
                       });
          })
          .RegisterType<TestScheduler, IHostedService>(DependencyScope.Single)
          .RegisterRepositories<IRepository>(new Assembly[] { repository })
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

         
        }
    }
}
