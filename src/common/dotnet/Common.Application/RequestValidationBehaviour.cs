using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DilemmaApp.Services.Common.Application
{
    public class RequestValidationBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}