using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres;
using DilemmaApp.Services.Dilemma.Infrastructure.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
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
                new AwsS3Bucket(Configuration["Infrastructure:S3:BaseURL"],
                    Configuration["Infrastructure:S3:Region"]));

            services.AddMediatR(typeof(GetDilemmaQuery).Assembly);

            services.AddDbContext<DilemmaContext>(options =>
                options.UseNpgsql(Configuration["Infrastructure:Postgres:ConnectionString"]));
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