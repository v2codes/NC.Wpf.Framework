using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var home = Properties.Resources.Home;
            var lang = CultureInfo.CurrentCulture.Name;
            if (lang == "zh-CN")
                lang = "en-US";
            else
                lang = "zh-CN";

            //App.ChangeLanguage(lang);
            var culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // Reload the MainWindow to apply the new language
            ReloadUI();
        }

        public void ReloadUI()
        {
            // 保存当前窗口大小和位置
            var oldSize = new { Width = Width, Height = Height };
            var oldPosition = new { Left = Left, Top = Top };

            // 重新加载窗口
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();

            // 关闭旧窗口
            Close();

            // 恢复窗口大小和位置
            newWindow.Width = oldSize.Width;
            newWindow.Height = oldSize.Height;
            newWindow.Left = oldPosition.Left;
            newWindow.Top = oldPosition.Top;
        }
    }
}