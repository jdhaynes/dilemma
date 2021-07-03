using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DilemmaApp.Common.Infrastructure.RabbitMqMessageBus
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private IPersistantRabbitMqConnection _connection;
        private string _exchangeName;

        public RabbitMqMessageBus(IPersistantRabbitMqConnection connection, string exchangeName)
        {
            _connection = connection;
            _exchangeName = exchangeName;
        }

        public void PublishIntegrationEvent(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.Connect();
            }

            using (IModel channel = _connection.OpenChannel())
            {
                string queueName = @event.GetType().Name;
                string eventJson = JsonConvert.SerializeObject(@event);
                byte[] body = Encoding.UTF8.GetBytes(eventJson);
                
                channel.ExchangeDeclare(
                    exchange: _exchangeName,
                    type: ExchangeType.Direct);

                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.QueueBind(
                    queue: queueName,
                    exchange: _exchangeName,
                    routingKey: queueName);
                
                channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            if (!_connection.IsConnected)
            {
                _connection.Connect();
            }

            using (IModel channel = _connection.OpenChannel())
            {
                string queueName = typeof(TEvent).Name;

                channel.ExchangeDeclare(
                    exchange: _exchangeName,
                    type: ExchangeType.Direct);

                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.QueueBind(
                    queue: queueName,
                    exchange: _exchangeName,
                    routingKey: queueName);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };

                channel.BasicConsume(
                    queue: queueName,
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}