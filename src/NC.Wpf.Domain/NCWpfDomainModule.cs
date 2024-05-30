//using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using NC.Wpf.Domain.Shared;

namespace NC.Wpf.Domain
{
    [DependsOn(// typeof(AbpDddDomainModule),
               typeof(NCWpfDomainSharedModule))]
    public class NCWpfDomainModule : AbpModule
    {

    }
}
