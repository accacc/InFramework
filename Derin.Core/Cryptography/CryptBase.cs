using Derin.Core.Configuration;
using System;

namespace Derin.Core.Cryptography
{
    public class CryptBase : ICryptBase
    {
        private Byte[] _KEY_64 = null;
        private Byte[] _IV_64 = null;
        protected virtual Byte[] KEY_64
        {
            get
            {
                if (_KEY_64 == null)
                {
                    _KEY_64 = Configuration.ConfigReader.GetCryptKEY(GetType().UnderlyingSystemType.Name);
                }
                return _KEY_64;
            }
        }

        public virtual Byte[] IV_64
        {
            get
            {
                if (_IV_64 == null)
                {
                    _IV_64 = ConfigReader.GetCryptIV(GetType().UnderlyingSystemType.Name);
                }
                return _IV_64;
            }
        }
        public virtual String Encrypt(String value)
        {
            return string.Empty;
        }
        public virtual String Decrypt(String value)
        {
            return string.Empty;
        }
    }
}
