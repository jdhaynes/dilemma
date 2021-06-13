namespace DilemmaApp.IdentitySvc.Application.Interfaces
{
    public interface IAuthTokenService
    {
        string GenerateToken(string userId);
        string ValidateToken(string token);
    }
}