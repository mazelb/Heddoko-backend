/**
 * @file PasswordHasher.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class Passphrase
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    public static class PasswordHasher
    {
        private const int SaltSize = 64;

        public static Passphrase Hash(string password)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = CreateRandomSalt();
            string hashedPassword = ComputeHash(passwordBytes, saltBytes);

            return new Passphrase
                   {
                       Hash = hashedPassword,
                       Salt = Convert.ToBase64String(saltBytes)
                   };
        }

        public static bool Equals(string password, string salt, string hash)
        {
            return string.CompareOrdinal(hash, Hash(password, salt)) == 0;
        }

        public static string GenerateRandomSalt(int size = SaltSize)
        {
            return Convert.ToBase64String(CreateRandomSalt(size));
        }

        private static string ComputeHash(byte[] password, byte[] salt)
        {
            byte[] passwordAndSalt = new byte[salt.Length + password.Length];

            Buffer.BlockCopy(salt, 0, passwordAndSalt, 0, salt.Length);
            Buffer.BlockCopy(password, 0, passwordAndSalt, salt.Length, password.Length);
            byte[] computedHash;
            using (HashAlgorithm algorithm = new SHA512Managed())
            {
                computedHash = algorithm.ComputeHash(passwordAndSalt);
            }
            return Convert.ToBase64String(computedHash);
        }

        private static string Hash(string password, string salt)
        {
            return ComputeHash(Encoding.Unicode.GetBytes(password), Convert.FromBase64String(salt));
        }

        private static byte[] CreateRandomSalt(int size = SaltSize)
        {
            byte[] saltBytes = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        public static string GenerateRandomPassword(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }

        public static string Md5(string src)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(src);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static string GenerateAccessToken(string email)
        {
            return Md5(email + DateTime.UtcNow.Ticks);
        }
    }
}