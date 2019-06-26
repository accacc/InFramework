using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Configuration
{
    public interface ICultureSettingsService
        : IBaseService
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
