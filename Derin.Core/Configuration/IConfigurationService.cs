using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Configuration
{
    public interface IConfigurationService: IBaseService
    {
        TConfig GetAppSettingsValue<TConfig>(string key);
        TConfig GetAppSettingsValue<TConfig>(string key,TConfig defaultValue);
    }
}
