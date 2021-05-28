namespace DilemmaApp.Services.Common.Application.RequestPipeline
{
    public enum ResponseState
    {
        Ok,
        ValidationError,
        DomainError,
        ServiceError
    }
}