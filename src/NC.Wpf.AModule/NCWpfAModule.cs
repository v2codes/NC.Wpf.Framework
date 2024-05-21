using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using NC.Wpf.Framework;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.AModule.Views;
using NC.AService;

namespace NC.Wpf.AModule
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(NCWpfFrameworkModule),
               typeof(NCAServiceContractsModule))]
    public class NCWpfAModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var regionManager = context.ServiceProvider.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(AView));
        }

    }
}
