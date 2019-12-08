using IF.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IF.Core.DependencyInjection.Interface
{


    public interface IInFrameworkBuilder
    {

        IInFrameworkBuilder RegisterAggregateService<TInterface>() where TInterface : class;

        IInFrameworkBuilder AddEmailLogger(Action<IEmailLoggerBuilder> action);
        IInFrameworkBuilder RegisterType<T, K>(DependencyScope scope);

        IInFrameworkBuilder AddServices<T>(Assembly[] assembly);

        //IInFrameworkBuilder AddRedisCache(Action<IRedisCacheBuilder> action);

        IInFrameworkBuilder Register<T>(T instance, DependencyScope scope);

        IInFrameworkBuilder AddEventBus(Action<IEventBusLogBuilder> action);

        IInFrameworkBuilder AddModule(Action<IModuleBuilder> action);
        IInFrameworkBuilder AddRazorTemplates(Action<IRazorTemlateBuilder> action);

        IInFrameworkBuilder AddLocalization(Action<ILocalizationBuilder> action);

        IInFrameworkBuilder AddApplicationLogger(Action<IApplicationLoggerBuilder> action);

        IInFrameworkBuilder AddIdentity(Action<IIdentityBuilder> action);

        IInFrameworkBuilder AddPerformanceLogger(Action<IPerformanceLoggerBuilder> action);
        IInFrameworkBuilder AddSmsLogger(Action<ISmsLoggerBuilder> action);

        IInFrameworkBuilder AddSmsSender(Action<ISmsBuilder> action);
        IInFrameworkBuilder AddRestClient(Action<IRestClientBuilder> action);
        IInFrameworkBuilder AddAuditLogger(Action<IAuditLoggerBuilder> action);

        IInFrameworkBuilder AddNotificationLogger(Action<INotificationLoggerBuilder> action);

        IInFrameworkBuilder AddEmailSender(Action<IEmailSenderBuilder> action);
        IInFrameworkBuilder AddNotificationSender(Action<INotificationBuilder> action);

        IInFrameworkBuilder AddMongo(Action<IMongoBuilder> action);

        void RegisterClosedType(Assembly[] assembly, Type type, DependencyScope scope);

        void RegisterImplementedInterface<T>(Assembly[] assembly,  DependencyScope scope);

        void Build();

        IInFrameworkBuilder AddCqrs(Action<ICqrsBuilder> action);
        void RegisterDecorators(Assembly[] assemblies, List<Type> handlers);

       

        IInFrameworkBuilder AddNewstonJson(Action<IJsonBuilder> action);
        //IInFrameworkBuilder AddApplicationSettings<Setting, ISettings>()
        //     where ISettings : IAppSettingsCore
        //     where Setting : ISettings;

        IInFrameworkBuilder AddApplicationSettings<ISettings>(ISettings settings) where ISettings : IAppSettingsCore;

        IInFrameworkBuilder RegisterIntance<T>(T instance, DependencyScope scope) where T : class;


    }
}
