using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Volo.Abp.DependencyInjection;

namespace NC.Wpf.App.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window, ISingletonDependency
    {

        public LoginWindow()
        {
            InitializeComponent();

            btnClose.Click += (s, e) =>
            {
                this.Close();
                System.Windows.Application.Current.Shutdown();
            };

            colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取系统的当前区域性
            CultureInfo culture = CultureInfo.CurrentCulture;

            // 根据当前区域性加载相应的资源文件
            if (culture.Name == "zh-CN")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
            }

            // 重新加载界面以应用新的语言
            // 清空现有界面元素，重新加载界面
            InitializeComponent();
        }
    }
}
