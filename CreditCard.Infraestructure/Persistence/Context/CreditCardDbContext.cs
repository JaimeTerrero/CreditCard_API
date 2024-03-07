using CreditCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Infraestructure.Persistence.Context
{
    public interface ICreditCardDbContext : IDbContext { }

    public class CreditCardDbContext : BaseDbContext
    {
        public CreditCardDbContext(DbContextOptions<CreditCardDbContext> options) : base(options) {}

        public DbSet<CreditCards> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
    }
}
