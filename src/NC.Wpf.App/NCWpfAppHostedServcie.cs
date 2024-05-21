using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Extensions.Hosting;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace NC.Wpf.App
{
    public class NCWpfAppHostedServcie<TApplication, TWindow> : IHostedService where TApplication : System.Windows.Application where TWindow : Window
    {
        private readonly TApplication _application;
        private readonly TWindow _window;
        private readonly IHostApplicationLifetime _appLifetime;

        public NCWpfAppHostedServcie(TApplication app,
                                     TWindow window,
                                     IHostApplicationLifetime appLifetime)
        {
            _application = app;
            _window = window;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 处理wpf应用退出时，停止主机
            _application.Exit += App_Exit;

            // 代码方式添加 App.xaml，主要是 MaterialDesignThemes 相关资源，但仅限 DynamicResources 使用，StaticResources 无法解析
            //GenerateAppResource(_application.MainWindow);

            _application.Run(_window);
            return Task.CompletedTask;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            //_app.Shutdown();
            _appLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_app.Shutdown();
            _appLifetime.StopApplication();
            return Task.CompletedTask;
        }

        #region 动态设置 App.xaml 
        /// <summary>
        /// 代码添加 MaterialDesignThemes 命名空间、资源，见App.xaml
        /// </summary>
        /// <param name="appNode"></param>
        private void GenerateAppResource(Window appNode)
        {
            var xmlns = new XmlNamespaceManager(new NameTable());
            xmlns.AddNamespace("materialDesign", "http://materialdesigninxaml.net/winfx/xaml/themes");
            appNode.RegisterName("materialDesign", "http://materialdesigninxaml.net/winfx/xaml/themes");
            _application.Resources = GetMaterialDesignThemesResources();
        }

        /// <summary>
        /// 添加 Resources
        /// </summary>
        /// <returns></returns>
        private ResourceDictionary GetMaterialDesignThemesResources()
        {
            // 创建一个新的 ResourceDictionary
            var resources = new ResourceDictionary();

            // 添加 Material Design 的主题资源
            var bundledTheme = new BundledTheme();
            bundledTheme.BaseTheme = BaseTheme.Light;
            bundledTheme.PrimaryColor = PrimaryColor.Green;
            bundledTheme.SecondaryColor = SecondaryColor.LightGreen;
            resources.MergedDictionaries.Add(bundledTheme);

            // 添加 MaterialDesignThemes UI 的默认资源字典
            var defaultsTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml"),
            };
            resources.MergedDictionaries.Add(defaultsTheme);

            //resources.Add("Style", GetMaterialDesignStyleResources());

            // 设置应用程序的资源字典为新创建的 ResourceDictionary
            return resources;
        }

        ///// <summary>
        ///// 添加 Style
        ///// </summary>
        ///// <returns></returns>
        //private Style GetMaterialDesignStyleResources()
        //{
        //    // 创建一个新的 Style 实例
        //    var buttonStyle = new Style(typeof(ShowMeTheXAML.XamlDisplay));
        //    // 设置 Style 的属性
        //    buttonStyle.BasedOn = (Style)Application.Current.TryFindResource(typeof(ShowMeTheXAML.XamlDisplay));
        //    // 创建一个 ResourceDictionary 实例，并添加子 ResourceDictionary
        //    var resourceDictionary = new ResourceDictionary();
        //    // 添加 MergedDictionaries
        //    resourceDictionary.MergedDictionaries.Add(new ResourceDictionary
        //    {
        //        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml")
        //    });
        //    resourceDictionary.MergedDictionaries.Add(new ResourceDictionary
        //    {
        //        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.ToggleButton.xaml")
        //    });

        //    // 将 ResourceDictionary 添加到 Style 的 Resources 中
        //    buttonStyle.Resources = resourceDictionary;

        //    // 将 Style 添加到 Application 节点中
        //    return buttonStyle;
        //}
        #endregion
    }
}
