using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lis.Common.Utils
{
    public class Cryptography
    {
        private string m_KeyInput;
        private byte[] m_Key;
        private byte[] m_IV;

        public Cryptography(string szKeyInput)
        {
            this.m_KeyInput = szKeyInput;

            m_IV = new byte[16];
            this.GetEncIV();

            m_Key = new byte[32];
            this.GetEncKey();

        }

        private void GetEncIV()
        {
            int IDLen = this.m_KeyInput.Length;
            int i;
            for (i = 0; i < 16; i++)
            {
                if (i < IDLen)
                    m_IV[i] = (byte)Convert.ToChar(this.m_KeyInput.Substring(i, 1));
                else
                    m_IV[i] = Convert.ToByte(15 - i);
            }
        }

        private void GetEncKey()
        {
            int IDLen = this.m_KeyInput.Length;
            int i;
            for (i = 0; i < 31; i++)
            {
                if (i < IDLen)
                    m_Key[i] = (byte)Convert.ToChar(this.m_KeyInput.Substring(IDLen - i - 1, 1));
                else
                    m_Key[i] = Convert.ToByte(i);
            }
        }

        public string Encrypt(string Original)
        {
            byte[] ToEncrypt;
            using (var MyRijndael = new RijndaelManaged())
            {
                MyRijndael.BlockSize = 128;
                MyRijndael.Mode = CipherMode.CBC;

                MyRijndael.Key = this.m_Key;
                MyRijndael.IV = this.m_IV;

                ICryptoTransform Encryptor = MyRijndael.CreateEncryptor();
                MemoryStream MsEncrypt = new MemoryStream();
                CryptoStream CsEncrypt = new CryptoStream(MsEncrypt, Encryptor, CryptoStreamMode.Write);
                ToEncrypt = Encoding.UTF8.GetBytes(Original);
                CsEncrypt.Write(ToEncrypt, 0, ToEncrypt.Length);
                CsEncrypt.FlushFinalBlock();
                return Convert.ToBase64String(MsEncrypt.ToArray());
            }
        }

        public string DeEncrypt(string Encrypted)
        {
            byte[] FromEncrypt;
            using (var MyRijndael = new RijndaelManaged())
            {
                MyRijndael.BlockSize = 128;
                MyRijndael.Mode = CipherMode.CBC;

                MyRijndael.Key = this.m_Key;
                MyRijndael.IV = this.m_IV;

                ICryptoTransform DeCryptor = MyRijndael.CreateDecryptor();
                MemoryStream MsDecrypt = new MemoryStream(Convert.FromBase64String(Encrypted));
                CryptoStream CsDecrypt = new CryptoStream(MsDecrypt, DeCryptor, CryptoStreamMode.Read);
                FromEncrypt = new byte[Convert.FromBase64String(Encrypted).Length];
                StreamReader sr = new StreamReader(CsDecrypt);
                return sr.ReadToEnd();
            }
        }
    }
}
