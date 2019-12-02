using Autofac;
using Autofac.Extras.AggregateService;
using IF.Configuration;
using IF.Core.Configuration;
using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using IContainer = Autofac.IContainer;

namespace IF.Dependency.AutoFac
{
    public class InFrameworkBuilder : IInFrameworkBuilder
    {
        internal readonly ContainerBuilder builder;
        internal IContainer container { get; private set; }



        public InFrameworkBuilder()
        {
            builder = new ContainerBuilder();

        }

        public IInFrameworkBuilder AddCqrs(Action<ICqrsBuilder> action)
        {
            //this.RegisterType<DispatcherWithDI, IDispatcher>(DependencyScope.PerInstance);            
            //builder.RegisterAggregateService<IHandlerFactory>();
            //builder.RegisterAggregateService<IElasticSearchHandlerFactory>();

            action(new CqrsBuilder(this));

            return this;
        }


        public IInFrameworkBuilder RegisterAggregateService<TInterface>() where TInterface : class
        {
            builder.RegisterAggregateService<TInterface>();
            return this;
        }

        public IInFrameworkBuilder AddServices<T>(Assembly[] assembly)
        {
            builder.RegisterAssemblyTypes(assembly).AssignableTo<T>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).InstancePerDependency();

            return this;
        }


        public IInFrameworkBuilder AddEventBus(Action<IEventBusLogBuilder> action)
        {
            action(new EventBusBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddMongo(Action<IMongoBuilder> action)
        {
            action(new MongoBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddEmailSender(Action<IEmailSenderBuilder> action)
        {
            action(new EmailBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddNotificationSender(Action<INotificationBuilder> action)
        {
            action(new NotificationBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddRazorTemplates(Action<IRazorTemlateBuilder> action)
        {
            action(new RazorTemlateBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddLocalization(Action<ILocalizationBuilder> action)
        {
            action(new LocalizationBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddApplicationLogger(Action<IApplicationLoggerBuilder> action)
        {
            action(new ApplicationLoggerBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddPerformanceLogger(Action<IPerformanceLoggerBuilder> action)
        {
            action(new PerformanceLoggerBuilder(this));
            return this;
        }


        public IInFrameworkBuilder AddSmsLogger(Action<ISmsLoggerBuilder> action)
        {
            action(new SmsLoggerBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddAuditLogger(Action<IAuditLoggerBuilder> action)
        {
            action(new AuditLoggerBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddEmailLogger(Action<IEmailLoggerBuilder> action)
        {
            action(new EmailLoggerBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddNotificationLogger(Action<INotificationLoggerBuilder> action)
        {
            action(new NotificationLoggerBuilder(this));
            return this;
        }


        //public IInFrameworkBuilder AddRedisCache(Action<IRedisCacheBuilder> action)
        //{
        //    action(new RedisCacheBuilder(this));

        //    return this;


        //}



        public IInFrameworkBuilder AddIdentity(Action<IIdentityBuilder> action)
        {
            action(new IdentityBuilder(this));

            return this;
        }

        public IInFrameworkBuilder AddNewstonJson(Action<IJsonBuilder> action)
        {
            action(new JsonBuilder(this));
            return this;


        }
        public IInFrameworkBuilder AddApplicationSettings<ISettings>(ISettings settings) where ISettings : IAppSettingsCore
            
        {
            this.RegisterType<AppSettingsCore, IAppSettingsCore>(DependencyScope.Single);
            //this.RegisterType<Setting, ISettings>(DependencyScope.Single);
            this.RegisterInstance<ISettings>(settings, DependencyScope.Single);
            return this;
        }


        public IInFrameworkBuilder RegisterInstance<T>(T instance, DependencyScope scope)
        {
            var reg = this.builder.Register<T>(c => instance).As<T>();

            switch (scope)
            {
                case DependencyScope.Single:
                    reg.SingleInstance();
                    break;

                case DependencyScope.PerScope:
                    reg.InstancePerLifetimeScope();
                    break;
                case DependencyScope.PerRequest:
                    reg.InstancePerRequest();
                    break;
                case DependencyScope.PerInstance:
                    reg.InstancePerDependency();
                    break;
                default:
                    reg.InstancePerRequest();
                    break;
            }

            return this;
        }

        public void RegisterClosedType(Assembly[] assembly, Type type, DependencyScope scope)
        {
            var reg = builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(type);

            switch (scope)
            {
                case DependencyScope.Single:
                    reg.SingleInstance();
                    break;

                case DependencyScope.PerScope:
                    reg.InstancePerLifetimeScope();
                    break;
                case DependencyScope.PerRequest:
                    reg.InstancePerRequest();
                    break;
                case DependencyScope.PerInstance:
                    reg.InstancePerDependency();
                    break;
                default:
                    reg.InstancePerRequest();
                    break;
            }
        }


        public IInFrameworkBuilder RegisterType<C, A>(DependencyScope scope)
        {
            var reg = builder.RegisterType<C>().As<A>();

            switch (scope)
            {
                case DependencyScope.Single:
                    reg.SingleInstance();
                    break;

                case DependencyScope.PerScope:
                    reg.InstancePerLifetimeScope();
                    break;
                case DependencyScope.PerRequest:
                    reg.InstancePerRequest();
                    break;
                case DependencyScope.PerInstance:
                    reg.InstancePerDependency();
                    break;
                default:
                    reg.InstancePerRequest();
                    break;
            }

            return this;
        }





        public void RegisterDecorators(Assembly[] assemblies, List<Type> decorators)
        {

            if (decorators.Count == 1)
            {
                builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(decorators[0]).InstancePerDependency();

            }
            else
            {
                builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(decorators[0], decorators[0].Name).InstancePerDependency();

                for (int i = 0; i < decorators.Count; i++)
                {
                    if (i == 0) continue;

                    if (decorators.Count == i + 1)
                    {
                        builder.RegisterGenericDecorator(decorators[i], decorators[0], fromKey: decorators[i - 1].Name).InstancePerDependency();
                    }
                    else
                    {
                        builder.RegisterGenericDecorator(decorators[i], decorators[0], fromKey: decorators[i - 1].Name, toKey: decorators[i].Name).InstancePerDependency();
                    }
                }

            }
        }

        public void Build()
        {
            this.container = this.builder.Build();
        }

        public IInFrameworkBuilder AddSmsSender(Action<ISmsBuilder> action)
        {
            action(new SmsBuilder(this));
            return this;
        }

        public IInFrameworkBuilder AddRestClient(Action<IRestClientBuilder> action)
        {
            action(new RestClientBuilder(this));
            return this;
        }

        public void RegisterImplementedInterface<T>(Assembly[] assembly, DependencyScope scope)
        {

            var reg = builder.RegisterAssemblyTypes(assembly).AssignableTo<T>().AsImplementedInterfaces();

            switch (scope)
            {
                case DependencyScope.Single:
                    reg.SingleInstance();
                    break;

                case DependencyScope.PerScope:
                    reg.InstancePerLifetimeScope();
                    break;
                case DependencyScope.PerRequest:
                    reg.InstancePerRequest();
                    break;
                case DependencyScope.PerInstance:
                    reg.InstancePerDependency();
                    break;
                default:
                    reg.InstancePerRequest();
                    break;
            }
        }
    }
}
