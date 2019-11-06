using IF.Configuration;
using IF.Core.Configuration;
using IF.Core.Database;
using IF.Core.MongoDb;
using IF.Core.RabbitMQ;
using IF.Core.Sms;
using System;

namespace IF.Template.Cqrs
{

    public interface IIFTemplateAppSettings : IAppSettingsCore
    {
        RabbitMQConnectionSettings RabbitMQConnection { get; set; }
        DatabaseSettings Database { get; set; }

        IFSmsSettings IFSms { get; set; }

        MongoConnectionSettings MongoConnection { get; set; }
    }

    public class IFTemplateAppSettings : AppSettingsCore, IIFTemplateAppSettings
    {

        public RabbitMQConnectionSettings RabbitMQConnection { get; set; }

        public IFSmsSettings IFSms { get; set; }

        public DatabaseSettings Database { get; set; }

        public MongoConnectionSettings MongoConnection { get; set; }

    }
}
