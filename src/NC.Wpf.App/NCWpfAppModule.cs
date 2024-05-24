using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using NC.Wpf.Framework;
using NC.Wpf.AModule;
using NC.Wpf.ControlModule;
using NC.Wpf.HomeModule;
using NC.AService;
using NC.Wpf.Application;
using NC.Wpf.EntityFrameworkCore;
using NC.Wpf.Application.Contracts;
using Volo.Abp;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Navigation.Regions;
using System;

namespace NC.Wpf.App
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(NCWpfFrameworkModule),
               typeof(NCWpfHomeModule),
               typeof(NCWpfAModule),
               typeof(NCWpfControlModule),
               typeof(NCAServiceModule),
               typeof(NCWpfEntityFrameworkCoreModule),
               typeof(AbpEntityFrameworkCoreSqlServerModule),
               typeof(NCWpfApplicationContractsModule),
               typeof(NCWpfApplicationModule))]
    public class NCWpfAppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //var hostingEnvironment = context.Services.GetHostingEnvironment();
            //var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            //Configure<AbpMultiTenancyOptions>(options =>
            //{
            //    options.IsEnabled = false;
            //});
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            //var app = context.ServiceProvider.GetService<System.Windows.Application>();
            //System.Windows.Application.LoadComponent(app, new Uri("App.xaml", UriKind.Relative));

            //var shell = context.ServiceProvider.GetService<Views.MainWindow>();
            //if (shell != null)
            //{
            //    RegionManager.SetRegionManager(shell, context.ServiceProvider.GetService<IRegionManager>());
            //    RegionManager.UpdateRegions();
            //}
        }
    }

    #region GetHostingEnvironment
    public static class AbpAspNetCoreServiceCollectionExtensions
    {
        public static IHostEnvironment GetHostingEnvironment(this IServiceCollection services)
        {
            var hostingEnvironment = services.GetSingletonInstanceOrNull<IHostEnvironment>();

            if (hostingEnvironment == null)
            {
                return new EmptyHostingEnvironment()
                {
                    EnvironmentName = Environments.Development
                };
            }

            return hostingEnvironment;
        }
    }

    class EmptyHostingEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = default!;

        public string ApplicationName { get; set; } = default!;

        public string WebRootPath { get; set; } = default!;

        public IFileProvider WebRootFileProvider { get; set; } = default!;

        public string ContentRootPath { get; set; } = default!;

        public IFileProvider ContentRootFileProvider { get; set; } = default!;
    }
    #endregion
}
