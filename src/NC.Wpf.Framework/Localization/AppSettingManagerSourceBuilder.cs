using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NC.Wpf.Framework.Localization
{
    internal class AppSettingManagerSourceBuilder
    {
        public string Code { get; private set; }

        public string FileName { get; private set; }

        public AppSettingManagerSourceBuilder(string nameSpace)
        {
            FileName = "AppSettingManager.g.cs";
            Code = "\r\nusing System.Linq;\r\nusing System.Configuration;\r\n\r\nnamespace " + nameSpace + "\r\n{\r\n    internal static class AppSettingManager\r\n    {\r\n        public static string Get(string key)\r\n        {\r\n            var appsettings = ConfigurationManager.AppSettings;\r\n            if((bool)appsettings.AllKeys?.Contains(key))\r\n            {\r\n                return ConfigurationManager.AppSettings[key];\r\n            }\r\n            return string.Empty;\r\n        }\r\n\r\n        public static void Set(string key, string value)\r\n        {\r\n            var configfile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);\r\n            var settings = configfile.AppSettings.Settings;\r\n            if (settings[key] == null)\r\n            {\r\n                settings.Add(key, value);\r\n            }\r\n            else\r\n            {\r\n                settings[key].Value = value;\r\n            }\r\n            configfile.Save(ConfigurationSaveMode.Modified);\r\n            ConfigurationManager.RefreshSection(configfile.AppSettings.SectionInformation.Name);\r\n        }\r\n    }\r\n}\r\n";
        }
    }

}
