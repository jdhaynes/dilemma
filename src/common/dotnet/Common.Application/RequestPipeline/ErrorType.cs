namespace DilemmaApp.Services.Common.Application.RequestPipeline
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        NotAuthenicated,
        NotAuthorized,
        Domain,
        Network,
        Application,
        Other
    }
}