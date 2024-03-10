using System.Configuration;
using System;
using Ncfe.CodeTest.Contracts;

namespace Ncfe.CodeTest.Services
{
    public class ConfigService : IConfigService
    {
        public string GetConfigString(string appSettingName)
        {
            var value = ConfigurationManager.AppSettings[appSettingName];

            if (string.IsNullOrEmpty(value))
            {
                throw new ApplicationException($"{appSettingName} does not exist.");
            }

            return value;
        }

        public int GetConfigInteger(string appSettingName)
        {
            int value;

            if (!int.TryParse(GetConfigString(appSettingName), out value))
            {
                throw new ApplicationException($"{appSettingName} failed to parse to integer.");
            }

            return value;
        }

        public bool GetConfigBoolean(string appSettingName)
        {
            bool value;

            if (!bool.TryParse(GetConfigString(appSettingName), out value))
            {
                throw new ApplicationException($"{appSettingName} failed to parse to bool.");
            }

            return value;
        }
    }
}
