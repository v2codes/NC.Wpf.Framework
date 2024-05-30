using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SqlSugar;
using SqlSugar.DistributedSystem.Snowflake;


//using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace NC.Wpf.Application
{
    public abstract class NCWpfAppService : ITransientDependency // : ApplicationService
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; } = default!;
        protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
        protected IUnitOfWork? CurrentUnitOfWork => UnitOfWorkManager?.Current;
        protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();

        /// <summary>
        /// 顺序Guid生成器
        /// </summary>
        protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);
        protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);
        protected Type? ObjectMapperContext { get; set; }
        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
            ObjectMapperContext == null
                ? provider.GetRequiredService<IObjectMapper>()
                : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

        /// <summary>
        /// 雪花Id生成器
        /// </summary>
        protected IdWorker LongIdGenerator => SnowFlakeSingle.Instance;

        protected NCWpfAppService()
        {
            ObjectMapperContext = typeof(NCWpfApplicationModule);
        }

        //protected NCWpfAppService()
        //{
        //    ObjectMapperContext = typeof(NCWpfApplicationModule);
        //}
    }

}
