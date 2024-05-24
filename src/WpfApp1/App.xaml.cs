using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var langurage = "en-us";
            //CultureInfo.CurrentCulture = new CultureInfo(langurage);
            Thread.CurrentThread.CurrentCulture= new CultureInfo(langurage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langurage);
        }

        public static void ChangeLanguage(string lang)
        {
            var culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // Reload the MainWindow to apply the new language
            foreach (Window window in Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    mainWindow.InitializeComponent();
                }
            }
        }
    }

}
