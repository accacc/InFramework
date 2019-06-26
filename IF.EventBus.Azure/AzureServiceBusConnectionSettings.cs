using System;
using System.Collections.Generic;
using System.Text;

namespace IF.EventBus.Azure
{
    public class AzureServiceBusConnectionSettings
    {
        public string ConnectionString { get; }
        public string TopicName { get; }
        public string SubscriptionName { get; }

        public string QueueName { get; set; }
    }
}
