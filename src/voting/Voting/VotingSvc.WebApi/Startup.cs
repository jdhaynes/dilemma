using DilemmaApp.Common.Infrastructure.RabbitMqMessageBus;
using DilemmaApp.Services.Common.Application.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using VotingSvc.Application.IntegrationEvents.Inbound;

namespace VotingSvc.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory()
            {
                HostName = "localhost"
            });

            services.AddSingleton<IPersistantRabbitMqConnection, PersistantRabbitMqConnection>();
            services.AddSingleton<IMessageBus>(_ => new RabbitMqMessageBus(
                _.GetRequiredService<IPersistantRabbitMqConnection>(),
                "integration_events_2"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            IMessageBus bus = app.ApplicationServices.GetRequiredService<IMessageBus>();
            bus.Subscribe<DilemmaPostedEvent, DilemmaPostedEventHandler>();

        }
    }
}