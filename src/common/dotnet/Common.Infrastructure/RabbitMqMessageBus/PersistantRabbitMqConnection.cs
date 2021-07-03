using System;
using RabbitMQ.Client;

namespace DilemmaApp.Common.Infrastructure.RabbitMqMessageBus
{
    public class PersistantRabbitMqConnection : IPersistantRabbitMqConnection
    {
        public bool IsConnected => _connection != null && _connection.IsOpen && !_isDisposed;

        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _isDisposed;

        public PersistantRabbitMqConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _isDisposed = false;
        }

        public void Connect()
        {
            _connection = _connectionFactory.CreateConnection();
        }

        public IModel OpenChannel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException(
                    "RabbitMQ connection not available to open channel.");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            
            _connection.Dispose();
            _isDisposed = true;
        }
    }
}