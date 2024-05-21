using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace NC.Wpf.Application
{
    public abstract class NCWpfAppService : ApplicationService
    {
        //public IAbpLazyServiceProvider LazyServiceProvider { get; set; } = default!;
        //protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
        //protected IUnitOfWork? CurrentUnitOfWork => UnitOfWorkManager?.Current;
        //protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();
        //protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);
        //protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
        //protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);
        //protected Type? ObjectMapperContext { get; set; }
        //protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        //    ObjectMapperContext == null
        //        ? provider.GetRequiredService<IObjectMapper>()
        //        : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));


        //protected NCWpfAppService(IAbpLazyServiceProvider lazyServiceProvider)
        //{
        //    LazyServiceProvider = lazyServiceProvider;
        //    ObjectMapperContext = typeof(NCWpfApplicationModule);
        //}

        protected NCWpfAppService()
        {
            ObjectMapperContext = typeof(NCWpfApplicationModule);
        }
    }

}
