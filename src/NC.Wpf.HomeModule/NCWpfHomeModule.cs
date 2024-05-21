using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework;
using NC.Wpf.HomeModule.Views;

namespace NC.Wpf.HomeModule
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(NCWpfFrameworkModule))]
    public class NCWpfHomeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // context.Services.AddTransient<object, HomeView>();
            // context.Services.AddKeyedTransient<object, HomeView>(nameof(HomeView));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var regionManager = context.ServiceProvider.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(HomeView));
        }
    }
}
