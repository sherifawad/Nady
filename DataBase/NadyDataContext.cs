using Core.Models;
using DataBase.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using static System.Environment;

namespace DataBase
{
    public class NadyDataContext : DbContext, IDatabaseContext
    {
        public NadyDataContext(DbContextOptions<NadyDataContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberDetails> MemberDetails { get; set; }
        public DbSet<MemberHistory> MemberHistories { get; set; }
        public DbSet<MemberPayment> MemberPayments { get; set; }
        public DbSet<ScheduledPayment> ScheduledPayments { get; set; }
        public DbSet<MemberVisitor> MemberVisitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") modelBuilder.ApplyDataFixForSqlite();
        }
    }
}
