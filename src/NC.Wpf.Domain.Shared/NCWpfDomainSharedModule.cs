using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace NC.Wpf.Domain.Shared
{
    [DependsOn(// typeof(AbpValidationModule), 
               typeof(AbpDddDomainSharedModule))]
    public class NCWpfDomainSharedModule : AbpModule
    {
        //public override void ConfigureServices(ServiceConfigurationContext context)
        //{
        //Configure<AbpVirtualFileSystemOptions>(options =>
        //{
        //    options.FileSets.AddEmbedded<NCWpfDomainSharedModule>();
        //});

        //Configure<AbpLocalizationOptions>(options =>
        //{
        //    options.Resources
        //        .Add<AbpDemoResource>("en")
        //        .AddBaseTypes(typeof(AbpValidationResource))
        //        .AddVirtualJson("/Localization/NCWpf");
        //});

        //Configure<AbpExceptionLocalizationOptions>(options =>
        //{
        //    options.MapCodeNamespace("NCWpf", typeof(NCWpfResource));
        //});
        //}
    }
}
