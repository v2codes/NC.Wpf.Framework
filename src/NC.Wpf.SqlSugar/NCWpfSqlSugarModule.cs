using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using NC.Wpf.Domain;

namespace NC.Wpf.SqlSugar
{
    [DependsOn(typeof(AbpGuidsModule))]
    public class NCWpfSqlSugarModule : AbpModule
    {
        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var service = context.Services;
            var configuration = service.GetConfiguration();
            Configure<SqlSugarOptions>(configuration.GetSection("SqlSugar"));
            Configure<SqlSugarOptions>(options =>
            {
                options.ConnectionString = configuration.GetConnectionString(NCDbProperties.ConnectionStringName);
            });

            // 注入 SqlSugar
            service.AddSqlSugar();

            return Task.CompletedTask;
        }

        public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<SqlSugarOptions>>().Value;
            var _logger = context.ServiceProvider.GetRequiredService<ILogger<NCWpfSqlSugarModule>>();

            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("==========SQL Options:==========");
            sb.AppendLine($"ConnectionString：{options.ConnectionString}");
            sb.AppendLine($"DbType：{options.DbType.ToString()}");
            sb.AppendLine($"DataSeed：{options.DataSeed}");
            sb.AppendLine($"CodeFirst：{options.CodeFirst}");
            //sb.AppendLine($"MultiTenancy：{options.EnabledSaasMultiTenancy}");
            sb.AppendLine("===============================");
            _logger.LogInformation(sb.ToString());

            // 创建数据库、表
            //if (options.CodeFirst)
            //{
            //    context.ServiceProvider.CreateDatabase();
            //}
            
            // 初始化种子数据
            //if (options.DataSeed)
            //{
            //    await context.ServiceProvider.DataSeedAsync();
            //}
        }

    }
}
