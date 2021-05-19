using System;
using System.Collections.Generic;

namespace DilemmaApp.Services.Common.Application
{
    public class Response<TPayload>
    {
        public TPayload Payload { get; set; }

        public IReadOnlyCollection<ValidationMessage> ValidationMessages =>
            _validationMessages.AsReadOnly();
        
        private List<ValidationMessage> _validationMessages;

        public Response()
        {
            _validationMessages = new List<ValidationMessage>();
        }
        
        protected void AddValidationMessage(string field, string message)
        {
            NullOrEmptyCheckArgument(nameof(field), field);
            NullOrEmptyCheckArgument(nameof(message), message);
            
            _validationMessages.Add(new ValidationMessage(field, message));
        }

        protected void RaiseError(string errorCode, string message)
        {
            throw new NotImplementedException();
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