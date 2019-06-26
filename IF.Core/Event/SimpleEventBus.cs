//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IF.Core.Event
//{

//    //observation pattern
//    public class SimpleEventBus : ISimpleEventBus
//    {

//        private readonly Dictionary<string, List<IIFEventHandler>> _handlers;
//        private readonly List<Type> _eventTypes;

//        public SimpleEventBus()
//        {
//            _handlers = new Dictionary<string, List<IIFEventHandler>>();
//            _eventTypes = new List<Type>();
//        }

//        public void Notify<T>(T @event) where T: IFEvent
//        {
//            var eventName = typeof(T).Name;
//            if (!_handlers.ContainsKey(eventName))
//            {
//                return;
//            }

//            foreach (IIFEventHandler<T> eventHandler in _handlers[eventName])
//            {
//                eventHandler.Handle(@event);
//            }
//        }

//        public void Attach<T>(IIFEventHandler<T> handler) where T : IFEvent
//        {
//            var eventName = typeof(T).Name;
//            if (_handlers.ContainsKey(eventName))
//            {
//                _handlers[eventName].Add(handler);
//            }
//            else
//            {
//                _handlers.Add(eventName, new List<IIFEventHandler>());
//                _handlers[eventName].Add(handler);
//                _eventTypes.Add(typeof(T));
//            }
//        }

//        public void Detach<T>(IIFEventHandler<T> handler) where T : IFEvent
//        {
//            var eventName = typeof(T).Name;
//            if (_handlers.ContainsKey(eventName) && _handlers[eventName].Contains(handler))
//            {
//                _handlers[eventName].Remove(handler);

//                if (_handlers[eventName].Count == 0)
//                {
//                    _handlers.Remove(eventName);
//                    var eventType = _eventTypes.Single(e => e.Name == eventName);
//                    _eventTypes.Remove(eventType);

//                }
//            }
//        }
//    }
//}
