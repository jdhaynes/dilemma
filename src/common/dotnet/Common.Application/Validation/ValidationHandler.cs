using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DilemmaApp.Services.Common.Application.Validation
{
    public class ValidationHandler<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response, new()
    {
        private readonly IValidator<TRequest> _validator;
        private readonly ILogger<ValidationHandler<TRequest, TResponse>> _logger;

        public ValidationHandler(IValidator<TRequest> validator,
            ILogger<ValidationHandler<TRequest, TResponse>> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            List<ValidationFailure> validationMessages = _validator
                .Validate(request)
                .Errors;

            bool validationSuccessful = !validationMessages.Any();

            if (validationSuccessful)
            {
                _logger.LogInformation("Validation of {@RequestType} {@ValidationPassed}",
                    typeof(TRequest).Name, true);

                return await next();
            }

            TResponse response = new TResponse();
            foreach (ValidationFailure error in validationMessages)
            {
                response.AddValidationMessage(error.PropertyName, error.ErrorMessage);
            }

            _logger.LogInformation(
                "Validation of {@RequestType} {@ValidationPassed} with messages {@validationMessages}",
                typeof(TRequest).Name, false, response.ValidationMessages);

            return response;
        }
    }
}