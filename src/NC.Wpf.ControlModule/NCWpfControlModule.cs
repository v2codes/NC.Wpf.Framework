
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using NC.Wpf.Framework;
using NC.Wpf.ControlModule.Views;
using NC.Wpf.Framework.Extensions;
using NC.Wpf.Core.Navigation.Regions;

namespace NC.Wpf.ControlModule
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(NCWpfFrameworkModule))]
    public class NCWpfControlModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.RegisterForNavigation<ControlSample>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var regionManager = context.ServiceProvider.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ControlSample));
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            //var regionManager = context.ServiceProvider.GetRequiredService<IRegionManager>();
            //var region = regionManager.Regions["ContentRegion"];
            //region.RequestNavigate("ControlSample");
        }
    }
}
