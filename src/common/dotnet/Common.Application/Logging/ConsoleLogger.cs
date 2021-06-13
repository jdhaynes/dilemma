using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DilemmaApp.Services.Common.Application.Logging
{
    public class ConsoleLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            Console.Write("I've just run!");

            return next();
        }
    }
}