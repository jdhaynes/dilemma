using System;
using System.Collections.Generic;

namespace DilemmaApp.Services.Common.Application.RequestPipeline
{
    public class Response<TPayload> : Response
    {
        public TPayload Payload { get; private set; }
        
        public Response() : base() { }

        public Response(TPayload payload)
        {
            Payload = payload;
        }
    }
    public class Response
    {
        public Error Error { get; private set; }
        public ResponseState State { get; private set; }

        public IReadOnlyCollection<ValidationMessage> ValidationMessages =>
            _validationMessages.AsReadOnly();

        private List<ValidationMessage> _validationMessages;

        public Response()
        {
            _validationMessages = new List<ValidationMessage>();
            Error = null;
            State = ResponseState.Ok;
        }

        public void AddValidationMessage(string field, string message)
        {
            CheckArgumentNotNullOrEmpty(nameof(field), field);
            CheckArgumentNotNullOrEmpty(nameof(message), message);

            _validationMessages.Add(new ValidationMessage(field, message));
            State = ResponseState.ValidationError;
        }

        public void RaiseError(string errorCode, string message)
        {
            CheckArgumentNotNullOrEmpty(nameof(errorCode), errorCode);
            CheckArgumentNotNullOrEmpty(nameof(message), message);

            if (Error != null)
            {
                throw new Exception("Only one error can be raised.");
            }

            Error = new Error(errorCode, message);
        }

        private void CheckArgumentNotNullOrEmpty(string argName, string argValue)
        {
            if (String.IsNullOrEmpty(argValue))
            {
                throw new ArgumentException("Argument must not be null or empty.", argName);
            }
        }
    }
}