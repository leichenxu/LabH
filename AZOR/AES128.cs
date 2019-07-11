using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AZOR
{
    class AES128
    {
        /// <summary>
        /// Key for AES128.
        /// </summary>
        private static readonly string KEY = "FEP9DqbG7VTncqaS";
        /// <summary>
        /// Encryp the content and return in string.
        /// </summary>
        /// <param name="settings"></param>
        public static string Encrypt(string text)
        {
            //value to encrypt
            var valueToEncrypt = Encoding.UTF8.GetBytes(text);
            //create aes
            var aes = new RijndaelManaged();
            //initialize
            aes.IV = Encoding.UTF8.GetBytes(KEY);
            //key
            aes.Key = Encoding.UTF8.GetBytes(KEY);
            //set mode
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            //encryptor
            var cryptoTransform = aes.CreateEncryptor();
            //result
            var result = cryptoTransform.TransformFinalBlock(valueToEncrypt, 0, valueToEncrypt.Length);
            return Convert.ToBase64String(result, 0, result.Length);
        }
        /// <summary>
        /// Decrypt the content and return in string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            //null check
            if (text == null || text.Equals(""))
                //return
                return text;
            var valueToConvert = Convert.FromBase64String(text);
            //create aes
            var aes = new RijndaelManaged
            {
                //initialize
                IV = Encoding.UTF8.GetBytes(KEY),
                //key
                Key = Encoding.UTF8.GetBytes(KEY),
                //set mode
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            //create decryptor
            var cryptoTransform = aes.CreateDecryptor();
            //result
            var result = cryptoTransform.TransformFinalBlock(valueToConvert, 0, valueToConvert.Length);
            //return it
            return Encoding.UTF8.GetString(result);
        }
    }
}
