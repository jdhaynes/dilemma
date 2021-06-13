using System.Text;
using DilemmaApp.Services.Common.Application.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DilemmaApp.IdentitySvc.Infrastructure.Messaging
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private string _exchange;

        public RabbitMQMessageBus(string exchange)
        {
            _exchange = exchange;
        }

        public void PublishIntegrationEvent(IntegrationEvent @event)
        {
            string message = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(message);

            ConnectionFactory factory = new ConnectionFactory() {HostName = "localhost"};

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(_exchange, ExchangeType.Direct);
                
                channel.BasicPublish(exchange: _exchange,
                    routingKey: @event.GetType().Name,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}