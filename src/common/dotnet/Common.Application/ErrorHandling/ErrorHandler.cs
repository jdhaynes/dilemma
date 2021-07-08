using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DilemmaApp.Services.Common.Application.ErrorHandling
{
    public class ErrorHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response, new()
    {
        private ILogger<ErrorHandler<TRequest, TResponse>> _logger;

        public ErrorHandler(ILogger<ErrorHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

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

                _logger.LogInformation("Domain rule violation {@Rule}", exception.ErrorCode);

                return response;
            }
            // All other unhandled exceptions.
            catch (Exception exception)
            {
                TResponse response = new TResponse();
                response.RaiseError(ErrorType.Application,
                    "UNHANDLED_EXCEPTION", "A server error occured.");

                _logger.LogError(exception, "Unhandled exception");

                return response;
            }
        }
    }
}