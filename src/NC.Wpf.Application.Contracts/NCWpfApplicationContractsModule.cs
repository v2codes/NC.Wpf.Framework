
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using NC.Wpf.Domain.Shared;

namespace NC.Wpf.Application.Contracts
{
    [DependsOn(typeof(NCWpfDomainSharedModule),
               typeof(AbpDddApplicationContractsModule))]
    public class NCWpfApplicationContractsModule : AbpModule
    {

    }
}
