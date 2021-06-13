using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using DilemmaApp.IdentitySvc.Application.Interfaces;

namespace DilemmaApp.IdentitySvc.Application.Services
{
    public class Sha256PasswordService : IPasswordService
    {
        private int _saltByteLength;
        
        public Sha256PasswordService(int saltByteLength)
        {
            _saltByteLength = saltByteLength;
        }

        public string GenerateSalt()
        {
            using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[_saltByteLength];
                rand.GetNonZeroBytes(salt);
                return Encoding.UTF8.GetString(salt);
            }
        }

        public string GenerateHash(string password, string salt)
        {
            byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.AppendFormat("{0:X2}", b);
                } 
                
                return stringBuilder.ToString();
            }
        }

        public bool AuthenticatePassword(string password, string salt, string hash)
        {
            string hashedPassword = GenerateHash(password, salt);
            return hashedPassword == hash;
        }
    }
}