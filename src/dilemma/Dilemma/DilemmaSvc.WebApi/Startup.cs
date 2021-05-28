using DilemmaApp.Services.Common.Application.RequestPipeline.Logging;
using DilemmaApp.Services.Common.Application.RequestPipeline.Validation;
using DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma;
using DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres;
using DilemmaApp.Services.Dilemma.Infrastructure.S3;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DilemmaSvc.WebApi
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
            services.AddScoped<IDilemmaRepository, PostgresDilemmaRepository>();
            services.AddScoped<ISqlConnectionFactory>(_ =>
                new PostgresConnectionFactory(
                    Configuration["Infrastructure:Postgres:ConnectionString"]));
            services.AddScoped<IFileStore>(_ =>
                new AwsS3Bucket(Configuration["Infrastructure:S3:BucketName"],
                    Configuration["Infrastructure:S3:BucketRegion"]));

            // Register validators.
            services.AddTransient<IValidator<GetDilemmaQuery>, GetDilemmaQueryValidator>();
            services.AddTransient<IValidator<PostDilemmaCommand>, PostDilemmaCommandValidator>();
            services.AddTransient<IValidator<GetTopicsQuery>, GetTopicsQueryValidator>();
            services.AddTransient<IValidator<WithdrawDilemmaCommand>,
                WithdrawDilemmaCommandValidator>();

            // Register MediatR request pipeline and decorators.
            services.AddMediatR(typeof(GetDilemmaQuery).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ConsoleLogger<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationHandler<,>));

            services.AddDbContext<DilemmaContext>(options =>
            {
                options.UseNpgsql(Configuration["Infrastructure:Postgres:ConnectionString"]);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
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