using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NC.Wpf.App.Models
{
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
