using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class NotificationLoggerBuilder : INotificationLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public NotificationLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }
    }
}
