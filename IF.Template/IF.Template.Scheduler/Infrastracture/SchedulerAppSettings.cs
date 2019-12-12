using IF.Configuration;
using IF.Core.Configuration;
using IF.Core.Database;
using IF.Core.MongoDb;
using IF.Core.RabbitMQ;
using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Template.Scheduler.Infrastracture
{
    public interface ISchedulerAppSettings : IAppSettingsCore
    {

        IFSmsSettings IFSms { get; set; }

        DatabaseSettings Database { get; set; }

        MongoConnectionSettings MongoConnection { get; set; }
    }

    public class SchedulerAppSettings : AppSettingsCore, ISchedulerAppSettings
    {

        public RabbitMQConnectionSettings RabbitMQConnection { get; set; }

        public IFSmsSettings IFSms { get; set; }

        public DatabaseSettings Database { get; set; }

        public MongoConnectionSettings MongoConnection { get; set; }

    }
}
