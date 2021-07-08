using DilemmaApp.Common.Infrastructure.RabbitMqMessageBus;
using DilemmaApp.Services.Common.Application.ErrorHandling;
using DilemmaApp.Services.Common.Application.Messaging;
using DilemmaApp.Services.Common.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using VotingSvc.Application.Commands.CastVoteCommand;
using VotingSvc.WebApi.BackgroundTasks;

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
            
            services.AddHostedService<EventSubscriberService>();

            services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory()
            {
                HostName = Configuration["Infrastructure:RabbitMQ:Host"],
                UserName = Configuration["Infrastructure:RabbitMQ:User"],
                Password = Configuration["Infrastructure:RabbitMQ:Password"],
                Port = int.Parse(Configuration["Infrastructure:RabbitMQ:Port"])
            });

            services.AddTransient<IValidator<CastVoteCommand>,
                CastVoteCommandValidator>();

            services.AddSingleton<IPersistantRabbitMqConnection, PersistantRabbitMqConnection>();
            services.AddScoped<IMessageBus>(_ => new RabbitMqMessageBus(
                _.GetRequiredService<IPersistantRabbitMqConnection>(),
                _.GetRequiredService<ILogger<RabbitMqMessageBus>>(),
                Configuration["Infrastructure:RabbitMQ:ExchangeName"]
            ));

            services.AddMediatR(typeof(CastVoteCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandler<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationHandler<,>));
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

        }
    }
}