using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using NC.Wpf.Core.Navigation;
using NC.Wpf.Core.Navigation.Regions;

namespace NC.Wpf.Framework.Mvvm
{
    /// <summary>
    /// 继承 ObservableRecipient 接口，可启用 Message 接收
    /// </summary>
    public abstract class ViewModelBase : ObservableRecipient, IDestructible // ObservableObject
    {
        /// <summary>
        /// 强引用IMessenger实现类
        /// 通常用于希望消息接收者不被 GC 回收的场景
        /// </summary>
        public StrongReferenceMessenger StrongMessenger => StrongReferenceMessenger.Default;

        protected ViewModelBase()
        {
            // 启用 Message 接收
            IsActive = true;
        }

        public virtual void Destroy()
        {
        }
    }
}
