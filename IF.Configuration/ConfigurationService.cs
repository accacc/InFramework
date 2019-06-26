using IF.Core.Configuration;
using IF.Core.Convert;
using Microsoft.Extensions.Configuration;
using System;

namespace IF.Configuration
{
    public class ConfigurationServiceNetCore : IConfigurationService
    {

        Microsoft.Extensions.Configuration.IConfiguration configuration;

        public ConfigurationServiceNetCore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TSection GetSection<TSection>() where TSection : new()
        {
            var configurationValue = new TSection();

            var section = typeof(TSection).Name.Replace("Settings", string.Empty);
            configuration.GetSection(section).Bind(configurationValue);
            return configurationValue;
        }

        public TConfig GetValue<TConfig>(string key)
        {
            try
            {
                string configValue = String.Empty;

                if (this.configuration[key] != null)
                {
                    configValue = this.configuration[key];
                }

                if (string.IsNullOrEmpty(configValue))
                {
                    throw new System.Exception(key + " value not found");
                }

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
                string configValue = String.Empty;

                if (this.configuration[key] != null)
                {
                    configValue = this.configuration[key];
                }

                if (string.IsNullOrEmpty(configValue))
                {
                    return defaultValue;
                }

                return ConvertHelper.ConvertValue<TConfig>(configValue);
            }
            catch (System.Exception exp)
            {
                throw new System.Exception("Configiration File reading error  : " + exp.Message);
            }
        }

        
    }
}
