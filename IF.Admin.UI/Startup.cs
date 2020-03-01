using IF.Configuration;
using IF.Core.Database;
using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using IF.Core.EventBus.Log;
using IF.Core.MongoDb;
using IF.Dependency.AutoFac;
using IF.MongoDB.Integration;
using IF.MongoDB.Repository;
using IF.MongoDB.Repository.Interface;
using IF.MongoDB.Service;
using IF.Rest.Client.Integration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using IF.HealthChecks;
using IF.Core.RabbitMQ;
using IF.HealthChecks.RabbitMQ;
using IF.HealthChecks.SqlServer;
using IF.HealthChecks.Checks;

namespace IF.Admin.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                                    {
                                        options.LoginPath = "/Account/Login";
                                        options.LogoutPath = "/Account/Logout";
                                    });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //var cultureInfo = new CultureInfo("tr-TR");

            //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var mongoSettings = new MongoConnectionSettings();
            mongoSettings.ConnectionString =  Configuration.GetSection("MongoConnection:ConnectionString").Value;
            mongoSettings.Database =  Configuration.GetSection("MongoConnection:Database").Value;

            

            IInFrameworkBuilder @if = new InFrameworkBuilder();


            @if
            //.AddApplicationSettings<IGatewayAppSettings>(settings)
            .AddNewstonJson(json => json.Build())
            .AddMongo(m=>m.AddMongoPerServiceConnectionStrategy(mongoSettings,c=>
            {
                c.AddApplicationLogger();
                c.AddAuditLogger();
                c.AddPerformanceLogger();
                c.AddSmsLogger();
                c.AddAuditLogger();
                c.AddEmailLogger();
            }                
            ))
            .AddRestClient(a => a.AddFluent(services))            
            ;


            @if.RegisterType<MongoEventBusLogService, IEventLogService>(DependencyScope.Single);
            @if.RegisterType<MongoEventBusLogRepository,IMongoEventBusLogRepository>(DependencyScope.Single);

            var dbSetting = Configuration.GetSettings<DatabaseSettings>();

            services.AddHealthChecks(checks =>
            {
                var minutes = 1;

                if (int.TryParse(Configuration["HealthCheck:Timeout"], out var minutesParsed))
                {
                    minutes = minutesParsed;
                }

                checks.AddSqlCheck("IF", dbSetting.ConnectionString, TimeSpan.FromMinutes(minutes));

                RabbitMQConnectionSettings settings = Configuration.GetSettings<RabbitMQConnectionSettings>();

                checks.AddRabbitMQCheck("Rabbit MQ", settings);
                checks.AddMongoDbCheck("Mongo Db", mongoSettings.ConnectionString);

                //checks.AddElasticSearchLoggerCheck("Elastic Search", Configuration["ElasticsearchLog:Host"]);

                checks.AddHealthCheckGroup(
                    "Api",
                    group => group
                    .AddUrlCheck(Configuration["HealthCheck:SSO"] + "/HealthCheck/Status", "SSO Api", TimeSpan.FromMinutes(minutes))
                    .AddUrlCheck(Configuration["HealthCheck:Resource"] + "/HealthCheck/Status", "Resource Api", TimeSpan.FromMinutes(minutes))
                );

                //checks.AddHealthCheckGroup(
                //          "Memory",
                //         group => group.AddPrivateMemorySizeCheck(1)
                //                        .AddVirtualMemorySizeCheck(2)
                //                        .AddWorkingSetCheck(1)

                //                        .AddSystemStorageCheck("Linux",options =>
                //                        {
                //                            options.AddDrive("C:\\", 500000);
                //                            options.AddDrive("D:\\", 500000);
                //                        })

                //                        ,CheckStatus.Unhealthy
                //      );

                //checks.AddCheck("Long-running", async cancellationToken => { await Task.Delay(10000, cancellationToken); return HealthCheckResult.Healthy("I ran too long"); });
                //checks.AddCheck<CustomHealthCheck>("Custom");
            });


            return null;// services.Build(@if);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
