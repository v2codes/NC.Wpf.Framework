using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Volo.Abp.DependencyInjection;

namespace NC.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISingletonDependency
    {
        public MainWindow()
        {
            InitializeComponent();

            // 注册Region
            // 1. xaml中注册
            // 2. 后台代码注册：RegionManager.SetRegionName(Content, "ContentRegion");

            // 绑定标题栏按钮事件
            BindingButtonEvents();

            // 系统当前时间
            StartTimer();

            // 获取本机IPV4地址
            tbIPAddress.Text = $"IP  {GetLocalIpAddress()}";
        }

        private void BindingButtonEvents()
        {
            btnMinus.Click += (s, e) => { WindowState = WindowState.Minimized; };
            btnMaximize.Click += (s, e) =>
            {
                if (WindowState == WindowState.Maximized)
                {
                    gridMainWindow.Margin = new Thickness(0);
                    WindowState = WindowState.Normal;
                }
                else
                {
                    gridMainWindow.Margin = new Thickness(5);
                    WindowState = WindowState.Maximized;
                }
            };
            btnClose.Click += (s, e) => { this.Close(); };

            colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            colorZone.MouseDoubleClick += (s, e) =>
            {
                if (WindowState == WindowState.Maximized)
                {
                    gridMainWindow.Margin = new Thickness(0);
                    WindowState = WindowState.Normal;
                }
                else
                {
                    gridMainWindow.Margin = new Thickness(5);
                    WindowState = WindowState.Maximized;
                }
            };
        }

        private void StartTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                var dtNow = DateTime.Now;
                tbTime.Text = $"{dtNow.ToString("dddd  yyyy-MM-dd HH:mm:ss")}";
            };
            timer.Start();
        }

        private void RadioButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
            //var radioButton = sender as RadioButton;
            //var command = radioButton.Command;
            //command.Execute(radioButton.CommandParameter);
        }

        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns></returns>
        private string GetLocalIpAddress()
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();
            string ip = string.Empty;
            foreach (var adapter in adapters)
            {
                var adapterProperties = adapter.GetIPProperties();
                var allAddress = adapterProperties.UnicastAddresses;
                //这里是根据网络适配器名称找到对应的网络，adapter.Name即网络适配器的名称
                if (allAddress.Count > 0
                    && adapter.OperationalStatus == OperationalStatus.Up
                    && (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet || adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                {
                    foreach (var addr in allAddress)
                    {
                        if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ip = addr.Address.ToString();
                            break;
                        }
                    }
                }
            }
            return ip;
        }

    }
}