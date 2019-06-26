using IF.Core.Log;
using IF.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using IF.Core.Performance;
using IF.Core.DependencyInjection.Model;
using IF.Core.Notification;
using IF.MongoDB.Service;
using IF.Core.Sms;
using IF.MongoDB.Repository;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;

namespace IF.MongoDB
{
    public static class BuilderExtensions
    {
        public static ILoggerBuilder AddMongoLogger(this ILoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoLogService, ILogService>(DependencyScope.Single);
            logger.Builder.RegisterInstance<IMongoLogRepository>(new MongoLogRepository(url,db),DependencyScope.Single);
            return logger;
        }

        public static IPerformanceLoggerBuilder AddMongoPerformanceLogger(this IPerformanceLoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoPerformanceLogService, IPerformanceLogService>(DependencyScope.Single);

            logger.Builder.RegisterInstance<IMongoPerformanceLogRepository>(new MongoPerformanceLogRepository(url, db), DependencyScope.Single);
            return logger;
        }

        public static IAuditLoggerBuilder AddMongoAuditLogger(this IAuditLoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoAuditLogService, IAuditLogService>(DependencyScope.Single);

            logger.Builder.RegisterInstance<IMongoAuditLogRepository>(new MongoAuditLogRepository(url, db), DependencyScope.Single);
            return logger;
        }


        public static IEmailLoggerBuilder AddMongoEmailLogger(this IEmailLoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoEmailLogService, Core.Email.IEmailLogService>(DependencyScope.Single);

            logger.Builder.RegisterInstance<IMongoEmailLogRepository>(new MongoEmailLogRepository(url, db), DependencyScope.Single);
            return logger;
        }

        public static INotificationLoggerBuilder AddMongoNotificationLogger(this INotificationLoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoNotificationLogService, INotificationLogService>(DependencyScope.Single);

            logger.Builder.RegisterInstance<IMongoNotificationLogRepository>(new MongoNotificationLogRepository(url, db), DependencyScope.Single);
            return logger;
        }


        public static ISmsLoggerBuilder AddMongoSmsLogger(this ISmsLoggerBuilder logger, string url, string db)
        {

            logger.Builder.RegisterType<MongoSmsLogService, ISmsLogService>(DependencyScope.Single);

            logger.Builder.RegisterInstance<IMongoSmsLogRepository>(new MongoSmsLogRepository(url, db), DependencyScope.Single);
            return logger;
        }

        public static IEventBusBuilder AddMongoEventLogger(this IEventBusBuilder eventBusBuilder,string url,string db)
        {

            eventBusBuilder.Builder.RegisterInstance<IEventLogService>(new MongoEventLogService(url, db), DependencyScope.Single);
            return eventBusBuilder;
        }
    }
}
