using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DilemmaApp.Services.Common.Application.RequestPipeline.ErrorHandling
{
    public class ErrorHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}