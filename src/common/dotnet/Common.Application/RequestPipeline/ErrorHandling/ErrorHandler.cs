using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Domain;
using MediatR;

namespace DilemmaApp.Services.Common.Application.RequestPipeline.ErrorHandling
{
    public class ErrorHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response, new()
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (DomainRuleException exception)
            {
                TResponse response = new TResponse();
                response.RaiseError(ErrorType.Domain, exception.ErrorCode, exception.Message);

                return response;
            }
            // All other unhandled exceptions.
            catch (Exception exception)
            {
                TResponse response = new TResponse();
                response.RaiseError(ErrorType.Application, 
                    "UNHANDLED_EXCEPTION", "A server error occured.");

                return response;
            }
        }
    }
}