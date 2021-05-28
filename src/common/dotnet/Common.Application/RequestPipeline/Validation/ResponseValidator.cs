using FluentValidation;

namespace DilemmaApp.Services.Common.Application.RequestPipeline.Validation
{
    public class ResponseValidator<TPayload> : AbstractValidator<Response<TPayload>>
    {
        public ResponseValidator(IValidator<TPayload> payloadValidator)
        {
            RuleFor(x => x.Payload).SetValidator(payloadValidator);
        }
    }
}