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
    class HashingPassword
    {
        public string SaltGeneration()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            byte[] salt = new byte[24];
            rng.GetBytes(salt);
            string saltString = BitConverter.ToString(salt);
            return saltString;
        }

        public string HashPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            HashingPassword salt = new HashingPassword();
            byte[] saltByte = Encoding.UTF8.GetBytes(salt.SaltGeneration());
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            byte[] saltPassword = passwordByte;

            return saltPassword.ToString();
        }
    }
}