using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DilemmaApp.Services.Common.Application.Validation
{
    public class ValidationHandler<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response, new()
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationHandler(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            List<ValidationFailure> validationMessages = _validator
                .Validate(request)
                .Errors;

            if (!validationMessages.Any())
            {
                return await next();
            }

            TResponse response = new TResponse();
            foreach (ValidationFailure error in validationMessages)
            {
                response.AddValidationMessage(error.PropertyName, error.ErrorMessage);
            }

            return response;
        }
    }
}