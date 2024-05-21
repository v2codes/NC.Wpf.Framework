using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NC.Wpf.Framework.Localization
{
    internal class ResourceExtensionSourceBuilder
    {
        public string Code { get; private set; }

        public string FileName { get; private set; }

        public ResourceExtensionSourceBuilder(string nameSpace, string className, PropertyInfo[] properties)
        {
            string text = className + "Extension";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo propertyInfo = properties[i];
                string value = "\r\n" + propertyInfo.Accessibility + " " + propertyInfo.Type + " " + propertyInfo.Name + "\r\n{\r\n    get => " + className + "." + propertyInfo.Name + ";\r\n}\r\n";
                stringBuilder.Append(value);
            }
            FileName = text + ".g.cs";
            Code = $"\r\nusing System;\r\nusing System.ComponentModel;\r\nusing System.Reflection;\r\nusing System.Threading;\r\n\r\nnamespace {nameSpace}\r\n{{\r\n    public partial class {text} : INotifyPropertyChanged\r\n    {{\r\n        #region Events\r\n        public event PropertyChangedEventHandler PropertyChanged;\r\n        #endregion\r\n\r\n        #region Fields\r\n        private static readonly {text} _instance;\r\n        #endregion\r\n\r\n        #region Methods\r\n        private {text}() {{ }}\r\n\r\n        static {text}()\r\n        {{\r\n            _instance = new {text}();\r\n            _instance.Initialize();\r\n        }}\r\n\r\n        private void OnPropertyChanged(string propertyName)\r\n        {{\r\n            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));\r\n        }}\r\n\r\n        public void Initialize()\r\n        {{\r\n            var cultureName = AppSettingManager.Get(\"language\");\r\n            var cultureInfo = new System.Globalization.CultureInfo(cultureName);\r\n            Thread.CurrentThread.CurrentUICulture = cultureInfo;\r\n            Thread.CurrentThread.CurrentCulture = cultureInfo;\r\n        }}\r\n\r\n        private void SetCurrentCulture(string cultureName)\r\n        {{\r\n            AppSettingManager.Set(\"language\", cultureName);\r\n            var cultureInfo = new System.Globalization.CultureInfo(cultureName);\r\n            Thread.CurrentThread.CurrentUICulture = cultureInfo;      \r\n            Thread.CurrentThread.CurrentCulture = cultureInfo;\r\n        }}\r\n\r\n        private void OnAllPropertyChanged()\r\n        {{\r\n            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);\r\n            foreach (var property in properties)\r\n            {{\r\n                this.OnPropertyChanged(property.Name);\r\n            }}\r\n        }}\r\n        #endregion\r\n\r\n        #region Properties\r\n        public static {text} Instance\r\n        {{\r\n            get => _instance;\r\n        }}\r\n\r\n        public string CurrentCulture\r\n        {{\r\n            get {{ return Thread.CurrentThread.CurrentUICulture.Name; }}\r\n            set\r\n            {{\r\n                SetCurrentCulture(value);\r\n                OnAllPropertyChanged();\r\n            }}\r\n        }}\r\n\r\n        {stringBuilder}\r\n        #endregion\r\n    }}\r\n}}\r\n";
        }
    }

}
