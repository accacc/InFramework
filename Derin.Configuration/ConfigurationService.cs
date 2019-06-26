using IF.Core.Configuration;
using IF.Core.Convert;
using System;
using System.Configuration;
using System.Globalization;

namespace Derin.Configuration
{


    public class ConfigurationService : IConfigurationService
    {
        public TSection GetSection<TSection>() where TSection : new()
        {
            throw new NotImplementedException();
        }

        public TConfig GetValue<TConfig>(string key)
        {
            try
            {
                string configValue = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(configValue))
                    throw new System.Exception(key + " value not found");
                return ConvertHelper.ConvertValue<TConfig>(configValue);
            }
            catch (System.Exception exp)
            {
                throw new System.Exception("Configiration File reading error  : " + exp.Message);
            }
        }

        public TConfig GetValue<TConfig>(string key, TConfig defaultValue)
        {
            try
            {
                string configValue = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(configValue))
                    throw new System.Exception(key + " value not found");
                return ConvertHelper.ConvertValue<TConfig>(configValue);
            }
            catch
            {
                return defaultValue;
            }
        }

      
    }
}
