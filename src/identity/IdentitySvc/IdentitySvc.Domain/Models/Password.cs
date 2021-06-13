namespace DilemmaApp.IdentitySvc.Domain.Models
{
    public class Password
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public Password(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}