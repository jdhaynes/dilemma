using System;

namespace Common.Domain
{
    public class DomainRuleException : Exception
    {
        public string ErrorCode { get; }

        public DomainRuleException(string errorCode) : base()
        {
            if (string.IsNullOrEmpty(errorCode))
            {
                throw new ArgumentOutOfRangeException(
                    "An error code must be provided when raising a domain rule exception.", 
                    this
                );
            }

            ErrorCode = errorCode;
        }
    }
}