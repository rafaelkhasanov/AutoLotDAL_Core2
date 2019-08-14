using System;
using AutoLotDAL_Core2.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AutoLotDAL_Core2.EF
{
    public class AutoLotContext : DbContext
    {
        public AutoLotContext(DbContextOptions options) : base(options)
        {
        }

        internal AutoLotContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString =
                    @"server = (LocalDb)\MSSQLLocalDB; database=AutoLotCore2; Integrated Security = True; 
                                MultipleActiveResultSets = True; App = EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            }
        }

        public DbSet<AutoLotDAL_Core2.Models.CreditRisk> CreditRisks { get; set; }
        public DbSet<AutoLotDAL_Core2.Models.Customer> Customers { get; set; }
        public DbSet<AutoLotDAL_Core2.Models.Inventory> Cars { get; set; }
        public DbSet<AutoLotDAL_Core2.Models.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Создать индекс, включающий в себя несколько столбцов
            modelBuilder.Entity<CreditRisk>(entity => entity.HasIndex(e => new {e.FirstName, e.LastName}).IsUnique());
            //Установить параметр каскадирования на отношение
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Car)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public string GetTableName(Type type) => Model.FindEntityType(type).SqlServer().TableName;
    }
}
