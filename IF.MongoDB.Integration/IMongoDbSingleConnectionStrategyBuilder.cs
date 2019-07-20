using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Integration
{
    public interface  IMongoDbSingleConnectionStrategyBuilder
    {
        IApplicationLoggerBuilder AddApplicationLogger ();
        IPerformanceLoggerBuilder AddPerformanceLogger ();
        IAuditLoggerBuilder AddAuditLogger ();
        IEmailLoggerBuilder AddEmailLogger ();
        INotificationLoggerBuilder AddNotificationLogger ();
        ISmsLoggerBuilder AddSmsLogger ();
        IEventBusLogBuilder AddEventBusLogger ();
    }
}
