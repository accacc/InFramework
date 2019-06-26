using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Configuration
{
    public static class ConfigReader
    {
    
        #region Private Methods

        private static string GetValue(string Key)
        {
            string Value = ConfigurationManager.AppSettings[Key];
            if (!string.IsNullOrEmpty(Value))
            {
                return Value;
            }
            return string.Empty;
        }

        private static string GetString(string Key, string DefaultValue)
        {
            string Setting = GetValue(Key);
            if (!string.IsNullOrEmpty(Setting))
            {
                return Setting;
            }
            return DefaultValue;
        }

        private static bool GetBool(string Key, bool DefaultValue)
        {
            string Setting = GetValue(Key);
            if (!string.IsNullOrEmpty(Setting))
            {
                switch (Setting.ToLower())
                {
                    case "false":
                    case "0":
                    case "n":
                        return false;
                    case "true":
                    case "1":
                    case "y":
                        return true;
                }
            }
            return DefaultValue;
        }

        private static int GetInt(string Key, int DefaultValue)
        {
            string Setting = GetValue(Key);
            if (!string.IsNullOrEmpty(Setting))
            {
                int i;
                if (int.TryParse(Setting, out i))
                {
                    return i;
                }
            }
            return DefaultValue;
        }

        private static int GetInt(string Key)
        {
            string Setting = GetValue(Key);

            if (!string.IsNullOrEmpty(Setting))
            {
                int i;
                if (int.TryParse(Setting, out i))
                {
                    return i;
                }
            }

            return -1;
        }

        private static double GetDouble(string Key, double DefaultValue)
        {
            string Setting = GetValue(Key);
            if (!string.IsNullOrEmpty(Setting))
            {
                double d;
                if (double.TryParse(Setting, out d))
                {
                    return d;
                }
            }
            return DefaultValue;
        }

        private static byte GetByte(string Key, byte DefaultValue)
        {
            string Setting = GetValue(Key);
            if (!string.IsNullOrEmpty(Setting))
            {
                byte b;
                if (byte.TryParse(Setting, out b))
                {
                    return b;
                }
            }
            return DefaultValue;
        }

        #endregion

        public static byte[] GetCryptKEY(string algorithm)
        {
            var strKey = GetValue(string.Format("{0}_KEY", algorithm));
            var strKeyArray = strKey.Split(',');
            List<byte> result = new List<byte>();
            foreach (var item in strKeyArray)
            {
                result.Add(System.Convert.ToByte(item.Trim()));
            }
            return result.ToArray();
        }

        public static byte[] GetCryptIV(string algorithm)
        {
            var strIV = GetValue(string.Format("{0}_IV", algorithm));
            var strIVArray = strIV.Split(',');
            List<byte> result = new List<byte>();
            foreach (var item in strIVArray)
            {
                result.Add(System.Convert.ToByte(item.Trim()));
            }
            return result.ToArray();
        }

        public static string DefaultCrypter
        {
            get
            {
                string result = GetValue("DefaultCrypter");
                if (string.IsNullOrEmpty(result))
                {
                    throw new System.Exception("Web.config dosyasinda DefaultCypter tanimli degildir.");
                }
                return result;
            }
        }
      



        public static string Response3DUrl
        {
            get
            {
                string responseUrl = GetValue("Response3DUrl");
                if (string.IsNullOrEmpty(responseUrl))
                {
                    throw new System.Exception("Web.config dosyasinda Response3DUrl tanimli degildir.");
                }
                return responseUrl;
            }
        }

    }
}
