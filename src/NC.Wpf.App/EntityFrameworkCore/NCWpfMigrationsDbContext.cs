using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Wpf.Domain;
using Volo.Abp.EntityFrameworkCore;

namespace NC.Wpf.EntityFrameworkCore
{
    public class NCWpfMigrationsDbContext : AbpDbContext<NCWpfMigrationsDbContext>, INCDbContext
    {
        public DbSet<Sample> Samples { get; set; }

        public NCWpfMigrationsDbContext(DbContextOptions<NCWpfMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureNCWpf();
        }
    }

}
