
using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Framework.Mvvm;
using NC.Wpf.App.Models;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Events;

namespace NC.Wpf.App.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ISingletonDependency
    {
        /// <summary>
        /// 当前活动中的菜单
        /// </summary>
        private string CurrentMenuUrl { get; set; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 菜单列表
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<MenuInfo> _menuList;

        /// <summary>
        /// 模块消息，状态栏显示
        /// </summary>
        [ObservableProperty]
        private string _moduleMessage;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MenuList = GetMenuList();
            CurrentMenuUrl = "HomeView";
            ModuleMessage = "暂无消息";
        }

        // 订阅 Message
        protected override void OnActivated()
        {
            //Register<>第一个类型一般是自己的类型,第2个是接收数据的类型
            //Register方法第1个参数一般是this,第2个参数是一个方法,可以获取接收到的值
            Messenger.Register<MainWindowViewModel, string, string>(this, "ModuleMessageToken", OnReceiveModuleMessage);
            Messenger.Register<MainWindowViewModel, RequestMessageBase, string>(this, "ModuleMessageToken", (_, m) =>
            {
                // 收到消息后，会进行简单的判断，并回复消息
                ModuleMessage = m.Message;
                m.Reply("MainWindow：已收到消息！");
            });
        }

        protected override void OnDeactivated()
        {
            Messenger.Unregister<string, string>(this, "ModuleMessageToken");
            Messenger.Unregister<RequestMessageBase, string>(this, "ModuleMessageToken");
        }

        private void OnReceiveModuleMessage(object sender, string message)
        {
            FormatMessage(message);
        }

        private void FormatMessage(string msg)
        {
            ModuleMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {msg}";
        }

        [RelayCommand]
        public void NavigationRadioButtonChanged()
        {
            var checkedMenu = MenuList.FirstOrDefault(p => p.IsDefault == true);
            if (checkedMenu != null && !checkedMenu.Url.IsNullOrWhiteSpace())
            {
                // 导航到其他视图，回调更新菜单状态
                _regionManager.RequestNavigate("ContentRegion", checkedMenu.Url, (navigationResult) =>
                {
                    // 获取当前活动中的View
                    //var region = _regionManager.Regions["ContentRegion"];
                    //var view = region.ActiveViews.FirstOrDefault();

                    if (navigationResult.Success)
                    {
                        CurrentMenuUrl = checkedMenu.Url;
                    }
                    else
                    {
                        foreach (var menu in MenuList)
                        {
                            if (menu.Url == CurrentMenuUrl)
                            {
                                menu.IsDefault = true;
                            }
                            else
                            {
                                menu.IsDefault = false;
                            }
                        }
                        var json = JsonSerializer.Deserialize<ObservableCollection<MenuInfo>>(JsonSerializer.Serialize(MenuList));
                        MenuList = json;
                    }
                });
            }
        }

        private ObservableCollection<MenuInfo> GetMenuList()
        {
            var menus = new ObservableCollection<MenuInfo>()
            {
                new MenuInfo {Name="首页",Url="HomeView",Icon="HomeOutline", IsDefault=true},
                new MenuInfo {Name="用户管理",Url="AView",Icon="AccountSupervisorOutline", IsDefault=false},
                new MenuInfo {Name="设备管理",Url="HomeView",Icon="Lan", IsDefault=false},
                new MenuInfo {Name="报警中心",Url="AView",Icon="AlarmLightOutline", IsDefault=false},
                new MenuInfo {Name="日志中心",Url="HomeView",Icon="FormatListBulleted", IsDefault=false},
                new MenuInfo {Name="控制中心",Url="AView",Icon="TuneVerticalVariant", IsDefault=false},
                new MenuInfo {Name="系统配置",Url="ControlSample",Icon="CogOutline", IsDefault=false},
            };
            return menus;
        }

    }
}
