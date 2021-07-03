namespace DilemmaApp.IdentitySvc.Application.Interfaces
{
    public interface IPasswordService
    {
        public byte[] GenerateSalt();
        public byte[] GenerateHash(string password, byte[] salt);
        public bool AuthenticatePassword(string password, byte[] salt, byte[] hash);
    }
}