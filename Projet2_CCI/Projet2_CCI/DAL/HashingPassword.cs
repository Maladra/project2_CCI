//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace Projet2_CCI
{
    public class HashingPassword
    {
        static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public static byte[] SaltGeneration()
        {
            byte[] salt = new byte[24];
            rng.GetBytes(salt);
            return salt;
        }

        public static byte[] HashPasswordSalt(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256Managed.Create())
            {
                //byte[] saltByte = SaltGeneration();
                byte[] passwordByte = Encoding.UTF8.GetBytes(password);
                sha256.TransformBlock(passwordByte, 0, passwordByte.Length, null, 0);
                sha256.TransformBlock(salt, 0, salt.Length, null, 0);
                return sha256.Hash;
            }
        }
    }
}