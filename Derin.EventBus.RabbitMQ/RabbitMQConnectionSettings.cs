using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.EventBus.RabbitMQ
{
    public class RabbitMQConnectionSettings
    {
        public string EventBusConnection { get; set; }

        public string EventBusUserName { get; set; }

        public string EventBusPassword { get; set; }

        public string EventBusRetryCount { get; set; }

        public string SubscriptionClientName { get; set; }

        public string Port { get; set; }
    }
}
