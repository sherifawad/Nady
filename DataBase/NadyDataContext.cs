using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using static System.Environment;

namespace DataBase
{
    public class NadyDataContext : DbContext, IDatabaseContext
    {
        private readonly string _dbPath;



        public DbSet<Member> Members { get; set; }
        public DbSet<MemberDetails> MemberDetails { get; set; }
        public DbSet<MemberFollower> MemberFollowers { get; set; }
        public DbSet<MemberPayment> MemberPayments { get; set; }
        public DbSet<ScheduledPayment> ScheduledPayments { get; set; }
        public NadyDataContext(string dbPath)
        {

            _dbPath = dbPath ?? Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "DataBase.db3");
            //SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        public NadyDataContext(DbContextOptions<NadyDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

            //if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            //{
            //    modelBuilder.ApplyDataFixForSqlite();
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            try
            {
                //optionsBuilder.EnableSensitiveDataLogging();
                //optionsBuilder.UseSqlite($"Filename={_dbPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NadyDataContext>
    {
        public NadyDataContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Nady/appsettings.json")
            //    .Build();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../DataBase/dataSettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<NadyDataContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlite(connectionString);
            return new NadyDataContext(builder.Options);
        }
    }
}
