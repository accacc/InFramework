using System;
using System.Security.Cryptography;
using System.IO;

namespace Derin.Core.Cryptography
{
    public class TripleDes : CryptBase
    {
        public override string Encrypt(string value)
        {
            string result = string.Empty;
            if (value != "")
            {
                using (TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                        StreamWriter sw = new StreamWriter(cs);
                        sw.Write(value);
                        sw.Flush();
                        cs.FlushFinalBlock();
                        ms.Flush();
                        string retval = System.Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                        retval = retval.Replace('+', '-').Replace('/', '_');
                        result = retval;
                    }
                }
            }
            return result;
        }
        public override string Decrypt(string value)
        {
            string result = string.Empty;
            if (value != "")
            {
                using (TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider())
                {
                    value = value.Replace('-', '+').Replace('_', '/');
                    Byte[] buffer = System.Convert.FromBase64String(value);
                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                        StreamReader sr = new StreamReader(cs);
                        result = sr.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}
