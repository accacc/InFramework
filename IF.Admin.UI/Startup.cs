using IF.Configuration;
using IF.Core.Database;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.Core.RabbitMQ;
using IF.Core.Rest;
using IF.EventBus.Logging.EF;
using IF.HealthChecks;
using IF.HealthChecks.Checks;
using IF.HealthChecks.RabbitMQ;
using IF.HealthChecks.SqlServer;
using IF.MongoDB;
using IF.MongoDB.Repository;
using IF.MongoDB.Service;
using IF.Rest.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TutumluAnne.Log.AdminUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
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


            var mongoConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            var mongoDatabase = Configuration.GetSection("MongoConnection:Database").Value;


            services.AddTransient<IMongoLogRepository>(provider => new MongoLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IMongoAuditLogRepository>(provider => new MongoAuditLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IMongoPerformanceLogRepository>(provider => new MongoPerformanceLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IMongoEmailLogRepository>(provider => new MongoEmailLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IMongoNotificationLogRepository>(provider => new MongoNotificationLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IMongoSmsLogRepository>(provider => new MongoSmsLogRepository(mongoConnectionString, mongoDatabase));
            services.AddTransient<IEventLogService>(provider => new MongoEventLogService(mongoConnectionString, mongoDatabase));


            services.AddSingleton<IIFRestClient, IFRestClient>();
            services.AddSingleton<IIFFluentRestClient, IFFluentRestClient>();



            //var dbSetting = Configuration.GetSettings<DatabaseSettings>();


            

            //services.AddHealthChecks(checks =>
            //{
            //    var minutes = 1;

            //    if (int.TryParse(Configuration["HealthCheck:Timeout"], out var minutesParsed))
            //    {
            //        minutes = minutesParsed;
            //    }

            //    checks.AddSqlCheck("TutumluAnne", dbSetting.ConnectionString, TimeSpan.FromMinutes(minutes));

            //    RabbitMQConnectionSettings settings = Configuration.GetSettings<RabbitMQConnectionSettings>();

            //    checks.AddRabbitMQCheck("Rabbit MQ", settings);
            //    checks.AddMongoDbCheck("Mongo Db", mongoConnectionString);

            //    //checks.AddElasticSearchLoggerCheck("Elastic Search", Configuration["ElasticsearchLog:Host"]);

            //    checks.AddHealthCheckGroup(
            //        "Api",
            //        group => group
            //        .AddUrlCheck(Configuration["HealthCheck:CoreApiUrl"] + "/HealthCheck/Status", "Core Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:EmailApiUrl"] + "/HealthCheck/Status", "Email Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:SmsApiUrl"] + "/HealthCheck/Status", "Sms Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:MarketingUrl"] + "/HealthCheck/Status", "Market Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:NotificationUrl"] + "/HealthCheck/Status", "App Push Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:SearchingUrl"] + "/HealthCheck/Status", "Search Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:SchedulerUrl"] + "/HealthCheck/Status", "Scheduler Api", TimeSpan.FromMinutes(minutes))
            //        .AddUrlCheck(Configuration["HealthCheck:WebSiteUrl"], "Web Site", TimeSpan.FromMinutes(minutes))
            //    );

            //    //checks.AddHealthCheckGroup(
            //    //          "Memory",
            //    //         group => group.AddPrivateMemorySizeCheck(1)
            //    //                        .AddVirtualMemorySizeCheck(2)
            //    //                        .AddWorkingSetCheck(1)

            //    //                        .AddSystemStorageCheck("Linux",options =>
            //    //                        {
            //    //                            options.AddDrive("C:\\", 500000);
            //    //                            options.AddDrive("D:\\", 500000);
            //    //                        })

            //    //                        ,CheckStatus.Unhealthy
            //    //      );

            //    //checks.AddCheck("Long-running", async cancellationToken => { await Task.Delay(10000, cancellationToken); return HealthCheckResult.Healthy("I ran too long"); });
            //    //checks.AddCheck<CustomHealthCheck>("Custom");
            //});

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
