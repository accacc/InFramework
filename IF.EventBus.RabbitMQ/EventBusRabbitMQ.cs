using Autofac;
using IF.Core.EventBus;
using IF.Core.EventBus. Log;
using IF.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IF.EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        const string BROKER_NAME = "inframework_event_bus";
        private readonly IEventLogService eventLogService;
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogService _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "inframework_event_bus";
        private readonly int _retryCount;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(IEventLogService eventLogService, IRabbitMQPersistentConnection persistentConnection,ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager,ILogService logger ,string queueName = null, int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _autofac = autofac;
            _retryCount = retryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            this.eventLogService = eventLogService;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Publish(IFEvent @event)
        {           

            this.eventLogService.SaveEventAsync(@event, _queueName, EventStateEnum.Publishing);

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    this.eventLogService.SaveEventAsync(@event, _queueName, EventStateEnum.PublishedFailed);
                    _logger.Error(ex, _queueName + ":" + @event.GetType().Name, "rabbitmq_eventbus", "", new Guid(), "", "");
                });

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME,type: "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    channel.BasicPublish(exchange: BROKER_NAME,
                                     routingKey: eventName,
                                     mandatory: true,
                                     basicProperties: properties,
                                     body: body);
                    
                    this.eventLogService.SaveEventAsync(@event, _queueName, EventStateEnum.Published);
                });             
            }

            
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIFEventHandler
        {
            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IFEvent
            where TH : IIFEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _subsManager.AddSubscription<T, TH>();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: BROKER_NAME,
                                      routingKey: eventName);
                }
            }
        }

        public void Unsubscribe<T, TH>()
            where TH : IIFEventHandler<T>
            where T : IFEvent
        {
            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIFEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                 type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += async (model, ea) =>
            {
                try
                {                    

                    var eventName = ea.RoutingKey;

                    var message = Encoding.UTF8.GetString(ea.Body);

                    await ProcessEvent(eventName, message);

                    channel.BasicAck(ea.DeliveryTag, multiple: false);

                    
                }
                catch (Exception ex)
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    var @event = (IFEvent)JsonConvert.DeserializeObject(message, typeof(IFEvent));
                    this.eventLogService.SaveEventAsync(@event, _queueName, EventStateEnum.PublishedFailed);
                    await _logger.ErrorAsync(ex, _queueName+":"+ ea.RoutingKey, "rabbitmq_eventbus", "",Guid.NewGuid(),"","");
                }

                
            };

            channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);

                    foreach (var subscription in subscriptions)
                    {
                        

                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIFEventHandler;
                            dynamic eventData = JObject.Parse(message);
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var @event = JsonConvert.DeserializeObject(message, eventType);
                            this.eventLogService.SaveEventAsync((IFEvent)@event, _queueName, EventStateEnum.Processing);
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            var concreteType = typeof(IIFEventHandler<>).MakeGenericType(eventType);
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                            this.eventLogService.SaveEventAsync((IFEvent)@event, _queueName, EventStateEnum.Processed);
                        }
                    }
                }
            }
        }
    }
}
