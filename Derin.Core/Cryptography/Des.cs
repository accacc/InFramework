using System;
using System.Security.Cryptography;
using System.IO;

namespace Derin.Core.Cryptography
{
    public class Des : CryptBase
    {
        public override string Encrypt(string value)
        {
            string result = string.Empty;

            if (value != "")
            {
                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                        StreamWriter sw = new StreamWriter(cs);
                        sw.Write(value);
                        sw.Flush();
                        cs.FlushFinalBlock();
                        ms.Flush();
                        int ada = (int)ms.Length;
                        result = System.Convert.ToBase64String(ms.GetBuffer(), 0, ada).ToString();
                    }
                }
            };
            return result;
        }
        public override string Decrypt(string value)
        {
            string result = string.Empty;
            if (value != "")
            {
                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    Byte[] buffer = System.Convert.FromBase64String(value);
                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                        StreamReader sr = new StreamReader(cs);
                        result = sr.ReadToEnd().ToString();
                    }
                }
            }
            return result;
        }
    }
}
