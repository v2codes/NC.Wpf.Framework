
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using SqlSugar;
using NC.Wpf.SqlSugar.Repositories;
using NC.Wpf.SqlSugar.DataSeed;

namespace NC.Wpf.SqlSugar
{
    public static class IServicesExtensions
    {
        public static IServiceCollection AddSqlSugar(this IServiceCollection services)
        {
            // 启用 AOT
            //StaticConfig.EnableAot = true;

            // 注入 SqlSugarClient 配置服务
            services.AddTransient<ISqlSugarDbConnectionCreator, SqlSugarDbConnectionCreator>();

            // 注入 ISqlSugarClient
            services.AddTransient<ISqlSugarClient>(serviceProvider =>
            {
                var sqlSugarDbConnectionCreator = serviceProvider.GetRequiredService<ISqlSugarDbConnectionCreator>();

                var sqlSugar = new SqlSugarScope(sqlSugarDbConnectionCreator.Build(),
                                                 sqlSugarClient =>
                                                 {
                                                     // 配置QueryFilter、AOP 事件
                                                     sqlSugarDbConnectionCreator.SetAopEvents(sqlSugarClient);
                                                 });

                return sqlSugar;
            });

            // 注入仓储
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // 注入种子数据服务
            services.AddTransient<IDataSeeder, NCDataSeederContributor>();

            return services;
        }

        public static IServiceProvider CreateDatabase(this IServiceProvider services)
        {
            var moduleContainer = services.GetRequiredService<IModuleContainer>();
            var sqlSugarClient = services.GetRequiredService<ISqlSugarClient>();

            //尝试创建数据库
            sqlSugarClient.DbMaintenance.CreateDatabase();

            var types = new List<Type>();
            foreach (var module in moduleContainer.Modules)
            {
                types.AddRange(module.Assembly.GetTypes()
                                              .Where(x => x.GetCustomAttribute<NotMappedAttribute>() == null)
                                              .Where(x => x.GetCustomAttribute<SugarTable>() != null)
                                              .Where(x => x.GetCustomAttribute<SplitTableAttribute>() is null));
            }
            if (types.Count > 0)
            {
                sqlSugarClient.CopyNew().CodeFirst.InitTables(types.ToArray());
            }

            return services;
        }

        public static async Task<IServiceProvider> DataSeedAsync(this IServiceProvider services)
        {
            var dataSeeder = services.GetService<IDataSeeder>();
            await dataSeeder?.SeedAsync();
            return services;
        }
    }
}
