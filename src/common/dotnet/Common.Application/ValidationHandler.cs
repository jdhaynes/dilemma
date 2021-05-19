using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DilemmaApp.Services.Common.Application
{
    public class ValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest> _validator;

        public ValidationHandler(IRequestHandler<TRequest, TResponse> inner,
            IValidator<TRequest> validator)
        {
            _inner = inner;
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            List<ValidationMessage> validationMessages = _validator.Validate(request).Errors
                .Select(e => new ValidationMessage(e.PropertyName, e.ErrorMessage))
                .ToList();

            if (validationMessages.Any())
            {
                
            }

            return _inner.Handle(request, cancellationToken);
        }
    }
}