using DilemmaApp.Common.Infrastructure;
using DilemmaApp.IdentitySvc.Application;
using DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand;
using DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand;
using DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.IdentitySvc.Application.Services;
using DilemmaApp.IdentitySvc.Infrastructure.Postgres;
using DilemmaApp.Services.Common.Application.ErrorHandling;
using DilemmaApp.Services.Common.Application.Interfaces;
using DilemmaApp.Services.Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DilemmaApp.IdentitySvc.WebApi
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

            services.AddScoped<IPasswordService>(_ =>
                new Sha256PasswordService(
                    int.Parse(Configuration["Cryptography:SaltLengthBytes"])));
            services.AddScoped<IAuthTokenService>(_ =>
                new JwtAuthTokenService(
                    Configuration["Cryptography:JwtTokenSecret"]));
            services.AddScoped<ISqlConnectionFactory>(_ =>
                new PostgresConnectionFactory(
                    Configuration["Infrastructure:Postgres:ConnectionString"]));
            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddTransient<IValidator<LoginUserCommand>,
                LoginUserCommandValidator>();
            services.AddTransient<IValidator<AuthenticateUserCommand>,
                AuthenticateUserCommandValidator>();
            services.AddTransient<IValidator<RegisterUserCommand>,
                RegisterUserCommandValidator>();

            services.AddMediatR(typeof(AuthenticateUserCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandler<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationHandler<,>));

            services.AddDbContext<IdentityContext>(options =>
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