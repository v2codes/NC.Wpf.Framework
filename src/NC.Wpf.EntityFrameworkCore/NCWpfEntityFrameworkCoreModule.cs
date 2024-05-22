using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using NC.Wpf.Domain;

namespace NC.Wpf.EntityFrameworkCore
{
    [DependsOn(typeof(NCWpfDomainModule),
               typeof(AbpEntityFrameworkCoreModule))]
    public class NCWpfEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NCDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }

    }
}
