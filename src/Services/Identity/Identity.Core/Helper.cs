using System.Security.Cryptography;

namespace Identity.Core
{
    public static class Helper
    {
        // Function to generate a random salt
        public static string GenerateSalt(int size = 16)
        {
            byte[] saltBytes = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Function to hash the password with the given salt
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashPassword, string salt)
        {
            var hashInputPassword = HashPassword(inputPassword, salt);

            return hashPassword == hashInputPassword;
        }
    }
}
