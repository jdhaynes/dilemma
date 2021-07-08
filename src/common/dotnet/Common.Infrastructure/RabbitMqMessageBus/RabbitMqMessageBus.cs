using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application.Messaging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DilemmaApp.Common.Infrastructure.RabbitMqMessageBus
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly IPersistantRabbitMqConnection _connection;
        private readonly ILogger<RabbitMqMessageBus> _logger;
        private string _exchangeName;
        private List<IModel> _subscriberChannels;

        public RabbitMqMessageBus(IPersistantRabbitMqConnection connection,
            ILogger<RabbitMqMessageBus> logger, string exchangeName)
        {
            _connection = connection;
            _logger = logger;
            _exchangeName = exchangeName;
            _subscriberChannels = new List<IModel>();
        }

        public void PublishIntegrationEvent(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.Connect();
            }

            using (IModel channel = _connection.OpenChannel())
            {
                _subscriberChannels.Add(channel);

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

                _logger.LogInformation(
                    "Integration event {EventType} published to {Queue} on {Exchange} with body {EventBody}",
                    queueName, queueName, _exchangeName, @event);
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

            IModel channel = _connection.OpenChannel();
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

            _logger.LogInformation("Subscribed to event {EventType}", queueName);

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Event {EventType} received from queue {MessageBody}",
                    queueName, message);

                channel.BasicAck(ea.DeliveryTag, true);
                return Task.CompletedTask;
            };

            channel.BasicConsume(
                queue: queueName,
                consumer: consumer);
        }

        public void Dispose()
        {
            if (_subscriberChannels != null)
            {
                _subscriberChannels.ForEach(x => x.Dispose());
            }
        }
    }
}