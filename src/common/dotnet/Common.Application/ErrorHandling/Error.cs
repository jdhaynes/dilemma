namespace DilemmaApp.Services.Common.Application.ErrorHandling
{
    public class Error
    {
        public ErrorType ErrorType { get; }
        public string ErrorCode { get; }
        public string Message { get; }

        public Error(ErrorType errorType, string errorCode, string message)
        {
            ErrorType = errorType;
            ErrorCode = errorCode;
            Message = message;
        }
    }
}