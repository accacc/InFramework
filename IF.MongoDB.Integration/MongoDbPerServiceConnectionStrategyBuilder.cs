using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Model;
using IF.Core.EventBus.Log;
using IF.Core.Log;
using IF.Core.Notification;
using IF.Core.Performance;
using IF.Core.Sms;
using IF.Dependency.AutoFac;
using IF.MongoDB.Repository;
using IF.MongoDB.Repository.Interface;
using IF.MongoDB.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Integration
{
    public class MongoDbPerServiceConnectionStrategyBuilder : IMongoDbConnectionStrategyBuilder
    {

        private readonly IInFrameworkBuilder dependencyInjection;
        public MongoDbPerServiceConnectionStrategyBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.dependencyInjection = dependencyInjection;
        }
        

        public  IApplicationLoggerBuilder AddApplicationLogger()
        {
            this.dependencyInjection.RegisterType<MongoApplicationLogService, ILogService>(DependencyScope.Single);
            this.dependencyInjection.RegisterType<MongoApplicationLogRepository,IMongoApplicationLogRepository>(DependencyScope.Single);
            return new ApplicationLoggerBuilder(this.dependencyInjection);
        }

        public  IPerformanceLoggerBuilder AddPerformanceLogger()
        {
            this.dependencyInjection.RegisterType<MongoPerformanceLogService, IPerformanceLogService>(DependencyScope.Single);

            this.dependencyInjection.RegisterType<MongoPerformanceLogRepository,IMongoPerformanceLogRepository>(DependencyScope.Single);
            return new PerformanceLoggerBuilder(this.dependencyInjection);
        }

        public  IAuditLoggerBuilder AddAuditLogger()
        {
            this.dependencyInjection.RegisterType<MongoAuditLogService, IAuditLogService>(DependencyScope.Single);
            this.dependencyInjection.RegisterType<MongoAuditLogRepository,IMongoAuditLogRepository>(DependencyScope.Single);
            return new AuditLoggerBuilder(this.dependencyInjection);
        }


        public  IEmailLoggerBuilder AddEmailLogger()
        {
            this.dependencyInjection.RegisterType<MongoEmailLogService, Core.Email.IEmailLogService>(DependencyScope.Single);

            this.dependencyInjection.RegisterType<MongoEmailLogRepository,IMongoEmailLogRepository>(DependencyScope.Single);
            return new EmailLoggerBuilder(this.dependencyInjection);
        }

        public  INotificationLoggerBuilder AddNotificationLogger()
        {
            this.dependencyInjection.RegisterType<MongoNotificationLogService, INotificationLogService>(DependencyScope.Single);

            this.dependencyInjection.RegisterType<MongoNotificationLogRepository,IMongoNotificationLogRepository>( DependencyScope.Single);
            return new NotificationLoggerBuilder(this.dependencyInjection);
        }


        public  ISmsLoggerBuilder AddSmsLogger()
        {

            this.dependencyInjection.RegisterType<MongoSmsLogService, ISmsLogService>(DependencyScope.Single);

            this.dependencyInjection.RegisterType<MongoSmsLogRepository,IMongoSmsLogRepository>( DependencyScope.Single);
            return new SmsLoggerBuilder(this.dependencyInjection);
        }

        public  IEventBusLogBuilder AddEventBusLogger( )
        {
            this.dependencyInjection.RegisterType<MongoEventBusLogService, IEventLogService>(DependencyScope.Single);
            this.dependencyInjection.RegisterType<MongoEventBusLogRepository,IMongoEventBusLogRepository>( DependencyScope.Single);
            return new EventBusBuilder(this.dependencyInjection);
        }
    }
}
