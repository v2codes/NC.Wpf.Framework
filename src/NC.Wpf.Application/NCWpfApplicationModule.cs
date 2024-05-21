
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using NC.Wpf.Application.Contracts;
using NC.Wpf.Domain;
using NC.Wpf.EntityFrameworkCore;

namespace NC.Wpf.Application
{
    [DependsOn(typeof(NCWpfDomainModule),
               typeof(NCWpfApplicationContractsModule),
               typeof(AbpDddApplicationModule),
               typeof(AbpAutoMapperModule),
               typeof(NCWpfEntityFrameworkCoreModule))]
    public class NCWpfApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NCWpfApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NCWpfApplicationModule>(validate: true);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
