using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using NC.Wpf.Application;
using NC.Wpf.Application.Contracts;
using NC.Wpf.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NC.Wpf.Sample;

[DependsOn(typeof(AbpAutofacModule),
           typeof(NCWpfEntityFrameworkCoreModule),
           typeof(AbpEntityFrameworkCoreSqlServerModule),
           typeof(NCWpfApplicationContractsModule),
           typeof(NCWpfApplicationModule))]
public class SampleModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<MainWindow>();

        // UseSqlServer
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });
    }
}
