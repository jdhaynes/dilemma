using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DilemmaApp.Services.Common.Application
{
    public class ConsoleLogger<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public ConsoleLogger(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Console.Write("I've just run!");

            return _inner.Handle(request, cancellationToken);
        }
    }
}