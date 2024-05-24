using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Framework.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace NC.Wpf.ControlModule.ViewModels
{
    public partial class ControlSampleViewModel : ViewModelBase, ITransientDependency
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _radioButtonChecked;

        /// <summary>
        /// 菜单列表
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<MenuInfo> _menuList;

        public ControlSampleViewModel()
        {
            MenuList = GetMenuList();
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

    public class MenuInfo
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 默认首页
        /// </summary>
        public bool? IsDefault { get; set; }

        ///// <summary>
        ///// 是否需要导航确认
        ///// </summary>
        //public bool? NavigationConfirm { get; set; }
    }
}
