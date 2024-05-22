using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using NC.Wpf.Domain;

namespace NC.Wpf.EntityFrameworkCore
{
    [ConnectionStringName(NCDbProperties.ConnectionStringName)]
    public class NCDbContext : AbpDbContext<NCDbContext>, INCDbContext
    {
        public DbSet<Sample> Samples { get; set; }

        public NCDbContext(DbContextOptions<NCDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNCWpf();
        }
    }
}
