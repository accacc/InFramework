using IF.Core.Configuration;
using IF.Core.DependencyInjection.Interface;
using IF.Core.DependencyInjection.Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IF.Core.DependencyInjection
{


    public interface IInFrameworkBuilder
    {

        IInFrameworkBuilder RegisterAggregateService<TInterface>() where TInterface : class;

        IInFrameworkBuilder AddEmailLogger(Action<IEmailLoggerBuilder> action);
        IInFrameworkBuilder RegisterType<T, K>(DependencyScope scope);

        IInFrameworkBuilder AddServices<T>(Assembly[] assembly);

        //IInFrameworkBuilder AddRedisCache(Action<IRedisCacheBuilder> action);

        IInFrameworkBuilder RegisterInstance<T>(T instance, DependencyScope scope);

        IInFrameworkBuilder AddEventBus(Action<IEventBusBuilder> action);
        IInFrameworkBuilder AddRazorTemplates(Action<IRazorTemlateBuilder> action);

        IInFrameworkBuilder AddLogger(Action<ILoggerBuilder> action);

        IInFrameworkBuilder AddIdentity(Action<IIdentityBuilder> action);

        IInFrameworkBuilder AddPerformanceLogger(Action<IPerformanceLoggerBuilder> action);
        IInFrameworkBuilder AddSmsLogger(Action<ISmsLoggerBuilder> action);

        IInFrameworkBuilder AddSmsSender(Action<ISmsBuilder> action);
        IInFrameworkBuilder AddRestClient(Action<IRestClientBuilder> action);
        IInFrameworkBuilder AddAuditLogger(Action<IAuditLoggerBuilder> action);

        IInFrameworkBuilder AddNotificationLogger(Action<INotificationLoggerBuilder> action);

        IInFrameworkBuilder AddEmailSender(Action<IEmailSenderBuilder> action);
        IInFrameworkBuilder AddNotificationSender(Action<INotificationBuilder> action);

        void RegisterClosedType(Assembly[] assembly, Type type, DependencyScope scope);

        void Build();

        IInFrameworkBuilder AddCqrs(Action<ICqrsBuilder> action);
        void RegisterDecorators(Assembly[] assemblies, List<Type> handlers);

       

        IInFrameworkBuilder AddNewstonJson(Action<IJsonBuilder> action);
        //IInFrameworkBuilder AddApplicationSettings<Setting, ISettings>()
        //     where ISettings : IAppSettingsCore
        //     where Setting : ISettings;

        IInFrameworkBuilder AddApplicationSettings<ISettings>(ISettings settings) where ISettings : IAppSettingsCore;
    }
}
