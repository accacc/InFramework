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
        ContainerBuilder autofacContainerBuilder;
        internal IContainer autofacContainer { get; private set; }

        public InFrameworkBuilder(ContainerBuilder builder)
        {
            this.autofacContainerBuilder = builder;

        }

        public InFrameworkBuilder()
        {
            this.autofacContainerBuilder = new ContainerBuilder();

        }

        public void SetContainerBuilder(ContainerBuilder builder)
        {
            this.autofacContainerBuilder = builder;
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
            autofacContainerBuilder.RegisterAggregateService<TInterface>();
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

        public IInFrameworkBuilder AddModule(Action<IModuleBuilder> action)
        {
            action(new ModuleBuilder(this));
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
            this.Register<ISettings>(settings, DependencyScope.Single);
            return this;
        }

        public IInFrameworkBuilder RegisterIntance<T>(T instance, DependencyScope scope) where T : class
        {
            var reg = this.autofacContainerBuilder.RegisterInstance(instance).As<T>();

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


        public IInFrameworkBuilder Register<T>(T instance, DependencyScope scope)
        {
            var reg = this.autofacContainerBuilder.Register<T>(c => instance).As<T>();

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

       

        public IInFrameworkBuilder RegisterType<C, A>(DependencyScope scope)
        {
            var reg = autofacContainerBuilder.RegisterType<C>().As<A>();

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





        public IInFrameworkBuilder RegisterDecorators(Assembly[] assemblies, List<Type> decorators)
        {

            if (decorators.Count == 1)
            {
                autofacContainerBuilder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(decorators[0]).InstancePerDependency();

            }
            else
            {
                autofacContainerBuilder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(decorators[0], decorators[0].Name).InstancePerDependency();

                for (int i = 0; i < decorators.Count; i++)
                {
                    if (i == 0) continue;

                    if (decorators.Count == i + 1)
                    {
                        autofacContainerBuilder.RegisterGenericDecorator(decorators[i], decorators[0], fromKey: decorators[i - 1].Name).InstancePerDependency();
                    }
                    else
                    {
                        autofacContainerBuilder.RegisterGenericDecorator(decorators[i], decorators[0], fromKey: decorators[i - 1].Name, toKey: decorators[i].Name).InstancePerDependency();
                    }
                }

            }

            return this;
        }

        public void Build()
        {
            this.autofacContainer = this.autofacContainerBuilder.Build();
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

        public IInFrameworkBuilder RegisterBaseInterfaceType<T>(Assembly[] assembly, DependencyScope scope)
        {

            var reg = autofacContainerBuilder.RegisterAssemblyTypes(assembly).AssignableTo<T>().AsImplementedInterfaces();

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

        public IInFrameworkBuilder RegisterClosedType(Assembly[] assembly, Type type, DependencyScope scope)
        {
            var reg = autofacContainerBuilder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(type);

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


        public IInFrameworkBuilder RegisterRepositories<T>(Assembly[] assembly)
        {
            return this.RegisterBaseInterfaceType<T>(assembly, DependencyScope.PerInstance);
        }

        public IInFrameworkBuilder RegisterBaseClassType<T>(Assembly[] assembly, DependencyScope scope)
        {
            var reg = autofacContainerBuilder.RegisterAssemblyTypes(assembly).AssignableTo<T>()
                //.PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerDependency();


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

    }
}
