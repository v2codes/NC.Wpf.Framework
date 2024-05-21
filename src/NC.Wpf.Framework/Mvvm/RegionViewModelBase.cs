using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NC.Wpf.Core.Navigation.Regions;

namespace NC.Wpf.Framework.Mvvm
{
    public class RegionViewModelBase : ViewModelBase, IConfirmNavigationRequest
    {
        private readonly IRegionManager _regionManager;

        public RegionViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            //if (MessageBox.Show("确认导航到其他视图?", "确认导航?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            //    result = false;

            continuationCallback(result);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
