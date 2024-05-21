using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using NC.Wpf.Core.Navigation;
using NC.Wpf.Core.Navigation.Regions;

namespace NC.Wpf.Framework.Mvvm
{
    /// <summary>
    /// 继承 ObservableRecipient 接口，可启用 Message 接收
    /// </summary>
    public abstract class ViewModelBase : ObservableRecipient, IDestructible // ObservableObject
    {
        public virtual void Destroy()
        {

        }        
    }
}
