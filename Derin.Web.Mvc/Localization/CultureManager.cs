using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Derin.Core.Mvc.Localization
{
    public static class CultureManager
    {
        
        static string _defaultCulture;

        static CultureInfo DefaultCulture
        {
            get
            {
                return SupportedCultures[_defaultCulture];
            }
        }

        static Dictionary<string, CultureInfo> SupportedCultures { get; set; }


        public static void AddSupportedCulture(string name)
        {
            SupportedCultures.Add(name.ToLowerInvariant(), CultureInfo.CreateSpecificCulture(name));
        }

        static void InitializeSupportedCultures()
        {
            SupportedCultures = new Dictionary<string, CultureInfo>();
        }

        static string ConvertToShortForm(string code)
        {
            //return code;
            return code.Substring(0, 2);
        }

        static bool CultureIsSupported(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            code = code.ToLowerInvariant();
            if (code.Length == 5)
                return SupportedCultures.ContainsKey(code);
            return CultureFormatChecker.FormattedAsCulture(code) && SupportedCultures.ContainsKey(ConvertToShortForm(code));
        }

        static CultureInfo GetCulture(string code)
        {
            if (!CultureIsSupported(code))
                return DefaultCulture;
            string shortForm = ConvertToShortForm(code).ToLowerInvariant(); ;
            return SupportedCultures[shortForm];
        }

        public static void SetCulture(string code)
        {
            CultureInfo cultureInfo = GetCulture(code);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public static void SetDefaultCulture(string code)
        {
            _defaultCulture = code;

        }

        static CultureManager()
        {
            InitializeSupportedCultures();
        }
    }
}
