using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Model;
using IF.Core.EventBus.Log;
using IF.Core.Log;
using IF.Core.Notification;
using IF.Core.Performance;
using IF.Core.Sms;
using IF.MongoDB.Repository;
using IF.MongoDB.Repository.Interface;
using IF.MongoDB.Service;

namespace IF.MongoDB.Integration
{


    public static class BuilderExtensions
    {

       

        public static ILoggerBuilder AddMongoApplicationLogger(this ILoggerBuilder logger, string url, string db)
        {
            logger.Builder.RegisterType<MongoApplicationLogService, ILogService>(DependencyScope.Single);
            logger.Builder.RegisterInstance< IMongoApplicationLogRepository>(new MongoApplicationLogRepository(url, db), DependencyScope.Single);
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

        public static IEventBusBuilder AddMongoEventLogger(this IEventBusBuilder eventLogBulder, string url, string db)
        {
            eventLogBulder.Builder.RegisterType<MongoEventLogService, IEventLogService>(DependencyScope.Single);
            eventLogBulder.Builder.RegisterInstance<IMongoEventLogRepository>(new MongoEventLogRepository(url, db), DependencyScope.Single);
            return eventLogBulder;
        }
    }
}
