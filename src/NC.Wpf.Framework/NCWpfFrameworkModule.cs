using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;
using NC.Wpf.Core;
using NC.Wpf.Core.Ioc;
using NC.Wpf.Core.Mvvm;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Navigation.Regions;
using NC.Wpf.Framework.Navigation.Regions.Behaviors;

namespace NC.Wpf.Framework
{
    [DependsOn(typeof(NCWpfCoreModule))]
    public class NCWpfFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

        }
    }
}
