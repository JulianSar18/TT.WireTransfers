using Microsoft.EntityFrameworkCore;
using TT.WireTransfers.Domain.Entities;

namespace TT.WireTransfers.Infraestructure.Persistence.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Movement> Movements => Set<Movement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>(wallet =>
            {
                wallet.HasKey(w => w.Id);
                wallet.Property(w => w.Id).ValueGeneratedOnAdd();
                wallet.Property(w => w.DocumentId).IsRequired().HasMaxLength(20);
                wallet.Property(w => w.Name).IsRequired().HasMaxLength(100);
                wallet.Property(w => w.Balance).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Movement>(movement =>
            {
                movement.HasKey(m => m.Id);
                movement.Property(m => m.Id).ValueGeneratedOnAdd();
                movement.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                movement.HasOne(m => m.Wallet).WithMany(w => w.Movements).HasForeignKey(m => m.WalletId);
            });
        }
    }
}
