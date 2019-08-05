using IF.Core.DependencyInjection.Interface;
using IF.Core.MongoDb;
using IF.MongoDB.Repository.Abstract;
using System;

namespace IF.MongoDB.Integration
{


    public static class BuilderExtensions
    {

        public static IMongoBuilder AddMongoSingleConnectionStrategy(this IMongoBuilder builder, MongoConnectionSettings settings, Action<IMongoDbConnectionStrategyBuilder> action)
        {
            builder.Container.RegisterInstance(settings, DependencyScope.Single);
            builder.Container.RegisterType<MongoDbSingleConnectionStrategy, IMongoDbConnectionStrategy>(DependencyScope.Single);
            action(new MongoDbSingleConnectionStrategyBuilder(builder.Container));
            return builder;
        }

        public static IMongoBuilder AddMongoPerServiceConnectionStrategy(this IMongoBuilder builder,MongoConnectionSettings settings ,Action<IMongoDbConnectionStrategyBuilder> action)
        {
            builder.Container.RegisterInstance(settings, DependencyScope.Single);
            builder.Container.RegisterType<Repository.Abstract.MongoDbPerServiceConnectionStrategy, IMongoDbConnectionStrategy>(DependencyScope.PerInstance);
            action(new MongoDbPerServiceConnectionStrategyBuilder(builder.Container));
            return builder;
        }

        //#region inject



        //public static IMongoBuilder AddApplicationLogger(this IMongoBuilder mongoBuilder, Action<IApplicationLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoApplicationLogService, ILogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoApplicationLogRepository,IMongoApplicationLogRepository>(DependencyScope.Single);
        //    action(new ApplicationLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}

        //public static IMongoBuilder AddMongoPerformanceLogger(this IMongoBuilder mongoBuilder, Action<IPerformanceLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoPerformanceLogService, IPerformanceLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoPerformanceLogRepository,IMongoPerformanceLogRepository>(DependencyScope.Single);
        //    action(new PerformanceLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}

        //public static IMongoBuilder AddMongoAuditLogger(this IMongoBuilder mongoBuilder, Action<IAuditLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoAuditLogService, IAuditLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoAuditLogRepository,IMongoAuditLogRepository>(DependencyScope.Single);
        //    action(new AuditLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}


        //public static IMongoBuilder AddMongoEmailLogger(this IMongoBuilder mongoBuilder, Action<IEmailLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoEmailLogService, Core.Email.IEmailLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoEmailLogRepository,IMongoEmailLogRepository>(DependencyScope.Single);
        //    action(new EmailLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}

        //public static IMongoBuilder AddMongoNotificationLogger(this IMongoBuilder mongoBuilder, Action<INotificationLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoNotificationLogService, INotificationLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoNotificationLogRepository,IMongoNotificationLogRepository>(DependencyScope.Single);
        //    action(new NotificationLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}


        //public static IMongoBuilder AddMongoSmsLogger(this IMongoBuilder mongoBuilder, Action<ISmsLoggerBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoSmsLogService, ISmsLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoSmsLogRepository,IMongoSmsLogRepository>(DependencyScope.Single);
        //    action(new SmsLoggerBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}

        //public static IMongoBuilder AddMongoEventLogger(this IMongoBuilder mongoBuilder, Action<IEventBusLogBuilder> action)
        //{
        //    mongoBuilder.Builder.RegisterType<MongoEventBusLogService, IEventLogService>(DependencyScope.Single);
        //    mongoBuilder.Builder.RegisterType<MongoEventBusLogRepository,IMongoEventBusLogRepository>(DependencyScope.Single);
        //    action(new EventBusBuilder(mongoBuilder.Builder));
        //    return mongoBuilder;
        //}


        //#endregion

        #region self inject
        //public static IApplicationLoggerBuilder AddMongoApplicationLogger(this IApplicationLoggerBuilder logger, string url, string db)
        //{
        //    logger.Builder.RegisterType<MongoApplicationLogService, ILogService>(DependencyScope.Single);
        //    logger.Builder.RegisterInstance< IMongoApplicationLogRepository>(new MongoApplicationLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}

        //public static IPerformanceLoggerBuilder AddMongoPerformanceLogger(this IPerformanceLoggerBuilder logger, string url, string db)
        //{
        //    logger.Builder.RegisterType<MongoPerformanceLogService, IPerformanceLogService>(DependencyScope.Single);

        //    logger.Builder.RegisterInstance<IMongoPerformanceLogRepository>(new MongoPerformanceLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}

        //public static IAuditLoggerBuilder AddMongoAuditLogger(this IAuditLoggerBuilder logger, string url, string db)
        //{
        //    logger.Builder.RegisterType<MongoAuditLogService, IAuditLogService>(DependencyScope.Single);

        //    logger.Builder.RegisterInstance<IMongoAuditLogRepository>(new MongoAuditLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}


        //public static IEmailLoggerBuilder AddMongoEmailLogger(this IEmailLoggerBuilder logger, string url, string db)
        //{
        //    logger.Builder.RegisterType<MongoEmailLogService, Core.Email.IEmailLogService>(DependencyScope.Single);

        //    logger.Builder.RegisterInstance<IMongoEmailLogRepository>(new MongoEmailLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}

        //public static INotificationLoggerBuilder AddMongoNotificationLogger(this INotificationLoggerBuilder logger, string url, string db)
        //{
        //    logger.Builder.RegisterType<MongoNotificationLogService, INotificationLogService>(DependencyScope.Single);

        //    logger.Builder.RegisterInstance<IMongoNotificationLogRepository>(new MongoNotificationLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}


        //public static ISmsLoggerBuilder AddMongoSmsLogger(this ISmsLoggerBuilder logger, string url, string db)
        //{

        //    logger.Builder.RegisterType<MongoSmsLogService, ISmsLogService>(DependencyScope.Single);

        //    logger.Builder.RegisterInstance<IMongoSmsLogRepository>(new MongoSmsLogRepository(url, db), DependencyScope.Single);
        //    return logger;
        //}

        //public static IEventBusLogBuilder AddMongoEventLogger(this IEventBusLogBuilder eventLogBulder, string url, string db)
        //{
        //    eventLogBulder.Builder.RegisterType<MongoEventBusLogService, IEventLogService>(DependencyScope.Single);
        //    eventLogBulder.Builder.RegisterInstance<IMongoEventBusLogRepository>(new MongoEventBusLogRepository(url, db), DependencyScope.Single);
        //    return eventLogBulder;
        //}
        #endregion
    }
}
