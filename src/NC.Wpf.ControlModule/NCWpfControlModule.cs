
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using NC.Wpf.Framework;
using NC.Wpf.ControlModule.Views;
//using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Extensions;
using NC.Wpf.ControlModule.ViewModels;

namespace NC.Wpf.ControlModule
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(NCWpfFrameworkModule))]
    public class NCWpfControlModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 注册视图导航
            context.Services.RegisterForNavigation<ControlSample>("ControlSample");

            //注册消息弹窗视图、绑定VM
            context.Services.RegisterDialog<MessageDialog, MessageDialogViewModel>();
            context.Services.RegisterDialog<ConfirmDialog, ConfirmDialogViewModel>();

            ////仅注册视图
            //context.Services.RegisterDialog<MessageDialog>();
            ////注册视图时绑定VM
            //context.Services.RegisterDialog<MessageDialog, MessageDialogViewModel>("MessageDialog");
            ////添加别名
            //context.Services.RegisterDialog<MessageDialog>("MessageDialog");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //var regionManager = context.ServiceProvider.GetService<IRegionManager>();
            //regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ControlSample));
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 设置默认导航首页
            //var regionManager = context.ServiceProvider.GetRequiredService<IRegionManager>();
            //regionManager.RequestNavigate("ContentRegion","ControlSample");
        }
    }
}
