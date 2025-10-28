using Microsoft.EntityFrameworkCore;
using TransactionApi.Models;

namespace TransactionApi.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });
        }
    }
}