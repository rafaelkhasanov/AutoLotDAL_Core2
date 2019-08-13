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
    }
}
