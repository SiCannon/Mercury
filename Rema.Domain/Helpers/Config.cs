using System;
using System.Configuration;

namespace Rema.Domain.Helpers
{
    class Config
    {
        public static string GetAppSetting(string settingKey)
        {
            if (String.IsNullOrEmpty(settingKey) || ConfigurationManager.AppSettings[settingKey] == null)
            {
                throw new ApplicationException("Setting '" + settingKey + "' does not exisit");
            }

            return ConfigurationManager.AppSettings[settingKey];
        }
    }
}
