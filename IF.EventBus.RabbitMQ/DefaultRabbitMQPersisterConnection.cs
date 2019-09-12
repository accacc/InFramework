using IF.Core.Log;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace IF.EventBus.RabbitMQ
{
    public class DefaultRabbitMQPersistentConnection
        : IRabbitMQPersistentConnection
    {

        private readonly List<AmqpTcpEndpoint> tcpEndpoints;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogService _logger;
        private readonly int _retryCount;
        IConnection _connection;
        bool _disposed;

        object sync_root = new object();

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogService logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            
            _retryCount = retryCount;

            this._logger = logger;
        }

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogService logger, List<AmqpTcpEndpoint> tcpEndpoints, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            _retryCount = retryCount;

            this._logger = logger;
            this.tcpEndpoints = tcpEndpoints;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.Error(ex,"rabbitmq_connection",ex.Message,"1",Guid.NewGuid(),"","");
            }
        }

        public bool TryConnect()
        {
            

            lock (sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.Error(ex, "rabbitmq_connection", ex.Message, "1", Guid.NewGuid(),"","");
                    }
                );

                policy.Execute(() =>
                {
                    if (tcpEndpoints != null && tcpEndpoints.Any())
                    {
                        _connection = _connectionFactory.CreateConnection(tcpEndpoints);
                    }
                    else
                    {
                        _connection = _connectionFactory.CreateConnection();
                    }
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    //_logger.Info("rabbitmq_connection",$"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events","1",Guid.NewGuid().ToString());

                    return true;
                }
                else
                {                    
                    _logger.Error("rabbitmq_connection", "FATAL ERROR: RabbitMQ connections could not be created and opened", "1", Guid.NewGuid(),"","");


                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            
            _logger.Warn("rabbitmq_connection", "FATAL ERROR : A RabbitMQ connection is shutdown. Trying to re-connect...", "1", Guid.NewGuid(),"","");
            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            
            _logger.Warn("rabbitmq_connection", "A RabbitMQ connection throw exception. Trying to re-connect...", "1", Guid.NewGuid(),"","");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;            
            _logger.Warn("rabbitmq_connection", "A RabbitMQ connection is on shutdown. Trying to re-connect...", "1", Guid.NewGuid(),"","");
            TryConnect();
        }
    }
}
