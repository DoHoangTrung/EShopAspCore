using EshopAspCore.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Data.EF
{
    public class EshopDbCoreContextFactory : IDesignTimeDbContextFactory<EshopDbContext>
    {
        public EshopDbContext CreateDbContext(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = Configuration.GetConnectionString(SystemConstants.MainConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<EshopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EshopDbContext(optionsBuilder.Options);
        }
    }
}
