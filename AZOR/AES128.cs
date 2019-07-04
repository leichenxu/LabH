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
        public static String Encrypt(object obj)
        {
            //value to encrypt
            var valueToEncrypt = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
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
        public static String Decrypt(object obj)
        {
            //value to descryp
            var valueToConvert = Convert.FromBase64String(obj.ToString());
            //create aes
            var aes = new RijndaelManaged();
            //initialize
            aes.IV = Encoding.UTF8.GetBytes(KEY);
            //key
            aes.Key = Encoding.UTF8.GetBytes(KEY);
            //set mode
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            //create decryptor
            var cryptoTransform = aes.CreateDecryptor();
            //result
            var result = cryptoTransform.TransformFinalBlock(valueToConvert, 0, valueToConvert.Length);
            //return it
            return Encoding.UTF8.GetString(result).ToString();
        }
    }
}
