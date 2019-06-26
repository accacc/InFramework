using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.EventBus
{

    public interface IIFEventHandler<in T> : IIFEventHandler
      where T : IFEvent
    {
        Task Handle(T @event);
    }

    public interface IIFEventHandler
    {
    }
    public interface IDynamicIFEventHandler
    {
        Task Handle(dynamic eventData);
    }

    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIFEventHandler;

        void AddSubscription<T, TH>()
           where T : IFEvent
           where TH : IIFEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IIFEventHandler<T>
             where T : IFEvent;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIFEventHandler;

        bool HasSubscriptionsForEvent<T>() where T : IFEvent;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IFEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
