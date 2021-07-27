using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<NadyDataContext>
    {
        public NadyDataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", true)
                 .AddEnvironmentVariables()
                 .Build();

            var builder = new DbContextOptionsBuilder<NadyDataContext>();

            var connectionString = configuration
                        .GetConnectionString("DefaultConnection");

            builder.UseSqlite(connectionString,
                        x => x.MigrationsAssembly(typeof(ApplicationDbContextFactory).Assembly.FullName));



            return new NadyDataContext(builder.Options);
        }
    }
}
