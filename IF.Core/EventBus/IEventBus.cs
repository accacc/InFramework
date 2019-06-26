using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.EventBus
{
    public interface IEventBus
    {
        void Publish(IFEvent @event);

        void Subscribe<T, TH>()
            where T : IFEvent
            where TH : IIFEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIFEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIFEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIFEventHandler<T>
            where T : IFEvent;
    }
}
