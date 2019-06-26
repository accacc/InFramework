using IF.Core.Configuration;
using System.Globalization;

namespace Derin.Configuration
{
    public class CultureSettingsService : ICultureSettingsService
    {

        private readonly IConfigurationService configService;
        public CultureSettingsService(IConfigurationService configService)
        {
            this.configService = configService;
        }

        public string CurrencyDecimalSeparator
        {
            get
            {
                return configService.GetValue<string>("CurrencyDecimalSeparator");
            }
        }

        public string DefaultLanguageCode
        {
            get
            {
                return configService.GetValue<string>("DefaultLanguageCode");
            }
        }

        public string CurrencyGroupSeparator
        {
            get
            {
                return configService.GetValue<string>("CurrencyGroupSeparator");
            }
        }

        public string FullDateTimePattern
        {
            get
            {
                return configService.GetValue<string>("FullDateTimePattern");
            }
        }

        public string NumberDecimalSeparator
        {
            get
            {
                return configService.GetValue<string>("NumberDecimalSeparator");
            }
        }

        public string NumberGroupSeparator
        {
            get
            {
                return configService.GetValue<string>("NumberGroupSeparator");
            }
        }

        public string PercentDecimalSeparator
        {
            get
            {
                return configService.GetValue<string>("PercentDecimalSeparator");
            }
        }

        public string PercentGroupSeparator
        {
            get
            {
                return configService.GetValue<string>("PercentGroupSeparator");
            }
        }

        public string ShortDatePattern
        {
            get
            {
                return configService.GetValue<string>("ShortDatePattern");
            }
        }


        public CultureInfo SetCultureSettings()
        {
            CultureInfo ci = new CultureInfo(this.DefaultLanguageCode);
            DateTimeFormatInfo dateformat = new DateTimeFormatInfo();
            dateformat.FullDateTimePattern = this.FullDateTimePattern;
            dateformat.ShortDatePattern = this.ShortDatePattern;
            ci.DateTimeFormat = dateformat;
            ci.NumberFormat.NumberDecimalSeparator = this.NumberDecimalSeparator;
            ci.NumberFormat.CurrencyDecimalSeparator = this.CurrencyDecimalSeparator;
            ci.NumberFormat.PercentDecimalSeparator = this.PercentDecimalSeparator;
            ci.NumberFormat.CurrencyGroupSeparator = this.CurrencyGroupSeparator;
            ci.NumberFormat.NumberGroupSeparator = this.NumberGroupSeparator;
            ci.NumberFormat.PercentGroupSeparator = this.PercentGroupSeparator;

            return ci;
        }

        
    }
}
