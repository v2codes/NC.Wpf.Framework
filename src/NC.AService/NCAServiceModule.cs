using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace NC.AService
{
    [DependsOn(typeof(NCAServiceContractsModule))]
    public class NCAServiceModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
