using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Common.Infrastructure.RabbitMqMessageBus;
using DilemmaApp.Services.Common.Application.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VotingSvc.Application.IntegrationEvents.Inbound;
using VotingSvc.Application.IntegrationEvents.Outbound;

namespace VotingSvc.WebApi.BackgroundTasks
{
    public class EventSubscriberService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IMessageBus _messageBus;

        public EventSubscriberService(IServiceProvider services)
        {
            _services = services.CreateScope().ServiceProvider;
            _messageBus = _services.GetService<IMessageBus>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBus.Subscribe<VoteCastEvent, VoteCastEventHandler>();
            
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _messageBus?.Dispose();
        }
    }
}