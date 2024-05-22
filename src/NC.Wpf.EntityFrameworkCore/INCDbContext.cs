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
    public interface INCDbContext : IEfCoreDbContext
    {
        /* 
         * Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<Sample> Samples { get; set; }
    }
}
