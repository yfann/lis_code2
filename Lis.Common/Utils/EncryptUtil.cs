using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lis.Common.Utils
{

    public static class EncryptUtil
    {
        private const string PasswordEncrypt_IV = "LIS20180101";
        private static readonly Cryptography crypt = new Cryptography(PasswordEncrypt_IV);
        public static string MD5HashWithIv(string str)
        {
            return MD5HashWithIv(str, PasswordEncrypt_IV);
        }

        /// <summary>
        /// 32 bits md5 hash
        /// </summary>
        /// <param name="str"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string MD5HashWithIv(string str, string IV)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var inputData = Encoding.UTF8.GetBytes(str + IV);
            md5.ComputeHash(inputData);
            var sb = new StringBuilder();
            foreach (var b in md5.Hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string AesEncrypt(string plainText)
        {
            return crypt.Encrypt(plainText);
        }

        public static string AesDecrypt(string password)
        {
            var result = "";
            try
            {
                result = crypt.DeEncrypt(password);

            }
            catch
            {
            }
            return result;
        }

        public static string Md5Hash(string text)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = md5.ComputeHash(bytes);
                var ret = BitConverter.ToString(hash).Replace("-", "");
                return ret;
            }
        }
    }
}
