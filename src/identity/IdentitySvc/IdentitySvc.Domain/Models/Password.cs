namespace DilemmaApp.IdentitySvc.Domain.Models
{
    public class Password
    {
        public byte[] Hash { get; private set; }
        public byte[] Salt { get; private set; }

        public Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}