using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ORMTest.Entity;

namespace ORMTest.DB
{
    public class DatabaseContext : DbContext
    {
      

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DatabaseSetting DatabaseSetting { get; }

         public DatabaseContext(DbContextOptions<DatabaseContext> options, DatabaseSetting databaseSetting)
            : base(options)
        {
            this.DatabaseSetting = databaseSetting;
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        // {
        //     options.UseSqlServer(DatabaseSetting.Connection);
        //     options.LogTo(Console.WriteLine, LogLevel.Information);
        // }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeID);
        
         }
    }
}