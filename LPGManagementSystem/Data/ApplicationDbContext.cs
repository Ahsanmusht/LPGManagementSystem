using Microsoft.EntityFrameworkCore;
using LPGManagementSystem.Models;

namespace LPGManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PartyType> PartyTypes { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<PettyCash> PettyCashes { get; set; }
        public DbSet<Assets> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and indexes
            modelBuilder.Entity<Party>()
                .HasOne(p => p.PartyType)
                .WithMany(pt => pt.Parties)
                .HasForeignKey(p => p.PartyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Item)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PettyCash>()
                .HasOne(pc => pc.Party)
                .WithMany(p => p.PettyCashes)
                .HasForeignKey(pc => pc.PartyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure computed columns
            modelBuilder.Entity<Purchase>()
                .Property(p => p.TotalAmount)
                .HasComputedColumnSql("[Price] * [Qty]", stored: true);

            modelBuilder.Entity<InvoiceItem>()
                .Property(ii => ii.Amount)
                .HasComputedColumnSql("[TotalKg] * [Price]", stored: true);

            // Configure indexes for performance
            modelBuilder.Entity<Party>()
                .HasIndex(p => p.PartyTypeId);

            modelBuilder.Entity<Purchase>()
                .HasIndex(p => p.SupplierId);

            modelBuilder.Entity<Purchase>()
                .HasIndex(p => p.TrDate);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.CustomerId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.Date);

            modelBuilder.Entity<InvoiceItem>()
                .HasIndex(ii => ii.InvoiceId);

            modelBuilder.Entity<InvoiceItem>()
                .HasIndex(ii => ii.ItemId);

            modelBuilder.Entity<PettyCash>()
                .HasIndex(pc => pc.PartyId);

            modelBuilder.Entity<PettyCash>()
                .HasIndex(pc => pc.TransactionDate);

            modelBuilder.Entity<Assets>()
                .HasIndex(a => a.IsInActive);

            // Configure unique constraints
            modelBuilder.Entity<PartyType>()
                .HasIndex(pt => pt.PartyTypeName)
                .IsUnique();
        }

        // Override SaveChanges to automatically update timestamps
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is PartyType partyType)
                    partyType.UpdatedAt = DateTime.Now;
                else if (entry.Entity is Party party)
                    party.UpdatedAt = DateTime.Now;
                else if (entry.Entity is Item item)
                    item.UpdatedAt = DateTime.Now;
                else if (entry.Entity is Purchase purchase)
                    purchase.UpdatedAt = DateTime.Now;
                else if (entry.Entity is Invoice invoice)
                    invoice.UpdatedAt = DateTime.Now;
                else if (entry.Entity is Assets assets)
                    assets.UpdatedAt = DateTime.Now;
            }
        }
    }
}