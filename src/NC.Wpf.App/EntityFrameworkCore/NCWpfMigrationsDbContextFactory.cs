using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NC.Wpf.EntityFrameworkCore
{
    public class NCWpfMigrationsDbContextFactory : IDesignTimeDbContextFactory<NCWpfMigrationsDbContext>
    {
        public NCWpfMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<NCWpfMigrationsDbContext>().UseSqlServer(configuration.GetConnectionString("NCService"));

            return new NCWpfMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
