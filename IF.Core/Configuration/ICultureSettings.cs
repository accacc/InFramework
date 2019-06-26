using System.Globalization;

namespace IF.Core.Configuration
{
    public interface ICultureSettingsService
    {

        string FullDateTimePattern { get; }
        string ShortDatePattern { get; }
        string NumberDecimalSeparator { get; }
        string CurrencyDecimalSeparator { get; }
        string PercentDecimalSeparator { get; }
        string CurrencyGroupSeparator { get; }
        string NumberGroupSeparator { get; }
        string PercentGroupSeparator { get; }

        CultureInfo SetCultureSettings();
    }
}
