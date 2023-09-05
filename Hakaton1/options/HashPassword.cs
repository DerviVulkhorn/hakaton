using System;
using System.Security.Cryptography;

namespace Hakaton1.options
{
    public class HashPassword
    {
        public string hashingPassword(string password)
        {
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);
            return Convert.ToBase64String(hashed);
        }
    }
}
