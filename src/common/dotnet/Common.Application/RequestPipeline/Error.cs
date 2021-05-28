namespace DilemmaApp.Services.Common.Application
{
    public class Error
    {
        public string ErrorCode { get; }
        public string Message { get; set; }

        public Error(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}