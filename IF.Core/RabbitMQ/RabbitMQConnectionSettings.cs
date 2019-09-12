using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.RabbitMQ
{
    public class RabbitMQConnectionSettings
    {
        public string EventBusConnection { get; set; }

        public string EventBusUserName { get; set; }

        public string EventBusPassword { get; set; }

        public string EventBusRetryCount { get; set; }

        //public string SubscriptionClientName { get; set; }

        public string Port { get; set; }
    }


    public class RabbitMQTcpConnectionSettings
    {

        public bool IsSslEnabled { get; set; }

        //public string EventBusConnection { get; set; }

        public string EventBusUserName { get; set; }

        public string EventBusPassword { get; set; }

        public string EventBusRetryCount { get; set; }

        public List<RabbitMQServer> Servers { get; set; }

        //public string SubscriptionClientName { get; set; }

        //public string Port { get; set; }
    }


    public class RabbitMQServer
    {
        public string EventBusConnection { get; set; }
        public int Port { get; set; }

        public bool SslEnabled { get; set; }
    }
}
