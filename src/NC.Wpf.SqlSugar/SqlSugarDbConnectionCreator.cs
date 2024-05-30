using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NC.Wpf.Domain;
using NC.Wpf.Domain.Entities;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace NC.Wpf.SqlSugar
{
    public class SqlSugarDbConnectionCreator : ISqlSugarDbConnectionCreator
    {
        private readonly SqlSugarOptions _sqlSugarOptions;
        private readonly IConfiguration _configuration;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ILogger _logger;


        public SqlSugarDbConnectionCreator(IOptions<SqlSugarOptions> sqlSugarOptions,
                                           IConfiguration configuration,
                                           IGuidGenerator guidGenerator,
                                           ILogger<SqlSugarDbConnectionCreator> logger)
        {
            _sqlSugarOptions = sqlSugarOptions.Value;
            _configuration = configuration;
            _guidGenerator = guidGenerator;
            _logger = logger;
        }

        public ConnectionConfig Build(Action<ConnectionConfig>? action = null)
        {
            #region 配置 SqlSugarOptions
            if (!_sqlSugarOptions.DbType.HasValue)
            {
                throw new ArgumentException("DbType配置为空");
            }

            //var slavaConFig = new List<SlaveConnectionConfig>();
            //if (Options.EnabledReadWrite)
            //{
            //    if (Options.ReadUrl is null)
            //    {
            //        throw new ArgumentException("读写分离为空");
            //    }

            //    var readCon = Options.ReadUrl;

            //    readCon.ForEach(s =>
            //    {
            //        //如果是动态saas分库，这里的连接串都不能写死，需要动态添加，这里只配置共享库的连接
            //        slavaConFig.Add(new SlaveConnectionConfig() { ConnectionString = s });
            //    });
            //}
            #endregion

            #region 配置 ConnectionConfig
            var connectionString = _configuration.GetConnectionString(NCDbProperties.ConnectionStringName);
            var connectionConfig = new ConnectionConfig()
            {
                //ConfigId = NCDbProperties.ConnectionStringName,// 多租户标识
                DbType = _sqlSugarOptions.DbType ?? DbType.SqlServer,
                ConnectionString = connectionString,
                IsAutoCloseConnection = true,
                //SlaveConnectionConfigs = slavaConFig,

                ConfigureExternalServices = new ConfigureExternalServices
                {
                    //设置 CodeFirst 非空值判断
                    EntityService = (c, p) =>
                    {
                        if (new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                        {
                            p.IsNullable = true;
                        }

                        EntityService(c, p);
                    },
                    //EntityService = (property, column) =>
                    //{
                    //    var attributes = property.GetCustomAttributes(true);//get all attributes 

                    //    if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey
                    //    {
                    //        column.IsPrimarykey = true; //有哪些特性可以看 1.2 特性明细
                    //    }
                    //    //可以写多个，这边可以断点调试
                    //    // if (attributes.Any(it => it is NotMappedAttribute))
                    //    //{
                    //    //    column.IsIgnore= true; 
                    //    //}
                    //},
                    //EntityNameService = (type, entity) =>
                    //{
                    //    var attributes = type.GetCustomAttributes(true);
                    //    if (attributes.Any(it => it is TableAttribute))
                    //    {
                    //        var attr = (attributes.First(it => it is TableAttribute) as TableAttribute);
                    //        entity.DbTableName = attr.Name;
                    //    }
                    //}
                },

                //这里多租户有个坑，无效的
                //AopEvents = new AopEvents
                //{
                //    DataExecuted = DataExecuted,
                //    DataExecuting = DataExecuting,
                //    OnLogExecuting = OnLogExecuting,
                //    OnLogExecuted = OnLogExecuted,
                //}
            };
            #endregion

            if (action is not null)
            {
                action.Invoke(connectionConfig);
            }


            return connectionConfig;
        }

        public void SetAopEvents(ISqlSugarClient sqlSugarClient)
        {
            sqlSugarClient.Aop.DataExecuting = this.DataExecuting;
            sqlSugarClient.Aop.DataExecuted = this.DataExecuted;
            sqlSugarClient.Aop.OnLogExecuting = this.OnLogExecuting;
            sqlSugarClient.Aop.OnLogExecuted = (sql, pars) =>
            {
                OnLogExecuted(sql, pars, sqlSugarClient);
            };
            AddTableFilters(sqlSugarClient);
        }

        #region AOP Events
        /// <summary>
        /// 上下文对象扩展
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public virtual void AddTableFilters(ISqlSugarClient sqlSugarClient)
        {
            //需自定义扩展
            //if (IsSoftDeleteFilterEnabled)
            //{
            sqlSugarClient.QueryFilter.AddTableFilter<ISoftDelete>(u => u.IsDeleted == false);
            //}
            //if (IsMultiTenantFilterEnabled)
            //{
            //    sqlSugarClient.QueryFilter.AddTableFilter<IMultiTenant>(u => u.TenantId == CurrentTenant.Id);
            //}
        }

        /// <summary>
        /// 实体配置
        /// </summary>
        /// <param name="property"></param>
        /// <param name="column"></param>
        public virtual void EntityService(PropertyInfo property, EntityColumnInfo column)
        {
            //if (property.PropertyType == typeof(ExtraPropertyDictionary)) // ABP ExtraProperty
            //{
            //    column.IsIgnore = true;
            //}
            if (property.Name == nameof(Entity<object>.Id))
            {
                column.IsPrimarykey = true;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="entityInfo"></param>
        public virtual void DataExecuting(object oldValue, DataFilterModel entityInfo)
        {
            //审计日志
            switch (entityInfo.OperationType)
            {
                case DataFilterType.UpdateByObject:

                    if (entityInfo.PropertyName.Equals(nameof(IAuditedAggregateRoot<Guid>.LastModificationTime)))
                    {
                        if (!DateTime.MinValue.Equals(oldValue))
                        {
                            entityInfo.SetValue(DateTime.Now);
                        }
                    }
                    //if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.LastModifierId)))
                    //{
                    //    if (CurrentUser.Id != null)
                    //    {
                    //        entityInfo.SetValue(CurrentUser.Id);
                    //    }
                    //}
                    break;
                case DataFilterType.InsertByObject:
                    if (entityInfo.PropertyName.Equals(nameof(IEntity<Guid>.Id)))
                    {
                        //主键为空或者为默认最小值
                        if (Guid.Empty.Equals(oldValue))
                        {
                            entityInfo.SetValue(_guidGenerator.Create());
                        }
                    }

                    if (entityInfo.PropertyName.Equals(nameof(IAuditedAggregateRoot<Guid>.CreationTime)))
                    {
                        //为空或者为默认最小值
                        if (oldValue is null || DateTime.MinValue.Equals(oldValue))
                        {
                            entityInfo.SetValue(DateTime.Now);
                        }
                    }

                    //if (entityInfo.PropertyName.Equals(nameof(IAuditedObject.CreatorId)))
                    //{
                    //    if (CurrentUser.Id != null)
                    //    {
                    //        entityInfo.SetValue(CurrentUser.Id);
                    //    }
                    //}

                    //插入时，需要租户id,先预留
                    //if (entityInfo.PropertyName.Equals(nameof(IMultiTenant.TenantId)))
                    //{
                    //    if (CurrentTenant is not null)
                    //    {
                    //        entityInfo.SetValue(CurrentTenant.Id);
                    //    }
                    //}
                    break;
            }


            //领域事件
            switch (entityInfo.OperationType)
            {
                case DataFilterType.InsertByObject:
                    //if (entityInfo.PropertyName == nameof(IEntity<object>.Id))
                    //{
                    //    EntityChangeEventHelper.PublishEntityCreatedEvent(entityInfo.EntityValue);
                    //}
                    break;
                case DataFilterType.UpdateByObject:
                    //if (entityInfo.PropertyName == nameof(IEntity<object>.Id))
                    //{
                    //    //软删除，发布的是删除事件
                    //    if (entityInfo.EntityValue is ISoftDelete softDelete)
                    //    {
                    //        if (softDelete.IsDeleted == true)
                    //        {
                    //            EntityChangeEventHelper.PublishEntityDeletedEvent(entityInfo.EntityValue);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        EntityChangeEventHelper.PublishEntityUpdatedEvent(entityInfo.EntityValue);
                    //    }
                    //}
                    break;
                case DataFilterType.DeleteByObject:
                    //if (entityInfo.PropertyName == nameof(IEntity<object>.Id))
                    //{
                    //    EntityChangeEventHelper.PublishEntityDeletedEvent(entityInfo.EntityValue);
                    //}
                    break;
            }

        }

        public virtual void DataExecuted(object oldValue, DataAfterModel entityInfo)
        {
            // do something
        }

        public virtual void OnLogExecuting(string sql, SugarParameter[] pars)
        {
            if (_sqlSugarOptions.SqlLog)
            {
                var sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("==========SQL执行:==========");
                sb.AppendLine(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars));
                sb.AppendLine("===============================");
                _logger.LogDebug(sb.ToString());
            }
        }

        public virtual void OnLogExecuted(string sql, SugarParameter[] pars, ISqlSugarClient sqlSugarClient)
        {
            if (_sqlSugarOptions.SqlLog)
            {
                var sqllog = $"=========SQL耗时{sqlSugarClient.Ado.SqlExecutionTime.TotalMilliseconds}毫秒=====";
                _logger.LogDebug(sqllog.ToString());
            }
        }

        #endregion
    }
}
