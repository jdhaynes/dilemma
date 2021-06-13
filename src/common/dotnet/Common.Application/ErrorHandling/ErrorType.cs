namespace DilemmaApp.Services.Common.Application.ErrorHandling
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