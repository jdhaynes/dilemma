namespace DilemmaApp.IdentitySvc.Application.Interfaces
{
    public interface IPasswordService
    {
        public string GenerateSalt();
        public string GenerateHash(string password, string salt);
        public bool AuthenticatePassword(string password, string salt, string hash);
    }
}