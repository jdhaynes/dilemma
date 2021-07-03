using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DilemmaApp.IdentitySvc.Application.Interfaces;

namespace DilemmaApp.IdentitySvc.Infrastructure.Crytography
{
    public class Sha256PasswordService : IPasswordService
    {
        private int _saltByteLength;
        
        public Sha256PasswordService(int saltByteLength)
        {
            _saltByteLength = saltByteLength;
        }

        public byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[_saltByteLength];
                rand.GetNonZeroBytes(salt);
                return salt;
            }
        }

        public byte[] GenerateHash(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = ConcatenateByteArray(passwordBytes, salt);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                return hashBytes;
            }
        }

        public bool AuthenticatePassword(string password, byte[] salt, byte[] hash)
        {
            byte[] hashedPassword = GenerateHash(password, salt);
            return hashedPassword.SequenceEqual(hash);
        }

        private byte[] ConcatenateByteArray(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, 0, second.Length);
            return bytes;
        }
    }
}