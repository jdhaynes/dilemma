using System;
using System.Collections.Generic;

namespace DilemmaApp.Services.Common.Application
{
    public class Response<TPayload>
    {
        public TPayload Payload { get; private set; }

        public Error Error { get; private set; }
        public IReadOnlyCollection<ValidationMessage> ValidationMessages =>
            _validationMessages.AsReadOnly();
        
        private List<ValidationMessage> _validationMessages;

        public Response(TPayload payload)
        {
            _validationMessages = new List<ValidationMessage>();
            Error = null;
            Payload = payload;
        }
        
        protected void AddValidationMessage(string field, string message)
        {
            NullOrEmptyCheckArgument(nameof(field), field);
            NullOrEmptyCheckArgument(nameof(message), message);
            
            _validationMessages.Add(new ValidationMessage(field, message));
        }

        protected void RaiseError(string errorCode, string message)
        {
            NullOrEmptyCheckArgument(nameof(errorCode), errorCode);
            NullOrEmptyCheckArgument(nameof(message), message);

            Error = new Error(errorCode, message);
        }

        private void NullOrEmptyCheckArgument(string argName, string argValue)
        {
            if (String.IsNullOrEmpty(argValue))
            {
                throw new ArgumentException("Argument must not be null or empty.", argName);
            }
        }
    }
}