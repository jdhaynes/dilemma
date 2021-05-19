namespace DilemmaApp.Services.Common.Application
{
    public class ValidationMessage
    {
        public string Property { get; }
        public string Message { get; }

        public ValidationMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }
    }
}