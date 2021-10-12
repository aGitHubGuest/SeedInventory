using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeedInventory.Models;
using System.Threading;
namespace SeedInventory.Repository
{
  public class InventoryContext : DbContext
  {
    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
      if (Database != null && Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
      {
        Database.SetCommandTimeout(9000);
      }
    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryRequest> InventoryRequest { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
      var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

      editedEntities.ForEach(e =>
      {
        if (e.Entity is IDateTraceable && e.State == EntityState.Modified)
        {
          var entry = ((IDateTraceable)e.Entity);
          entry.DateUpdated = DateTime.UtcNow;
        }
      });

      return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
      var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

      editedEntities.ForEach(e =>
      {
        if (e.Entity is IDateTraceable && e.State == EntityState.Modified)
        {
          var entry = ((IDateTraceable)e.Entity);
          entry.DateUpdated = DateTime.UtcNow;
        }
      });

      return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Inventory>().HasKey(i => new { i.Id });
      modelBuilder.Entity<Inventory>().Property(i => i.DateCreated).HasDefaultValueSql("GETUTCDATE()");
      modelBuilder.Entity<Inventory>().Property(i => i.DateUpdated).HasDefaultValueSql("GETUTCDATE()");
      modelBuilder.Entity<Inventory>().Property(i => i.Kernels).HasDefaultValueSql("0");

      modelBuilder.Entity<InventoryRequest>().HasKey(i => new { i.Id });
      modelBuilder.Entity<InventoryRequest>().Property(i => i.DateCreated).HasDefaultValueSql("GETUTCDATE()");
      modelBuilder.Entity<InventoryRequest>().Property(i => i.DateUpdated).HasDefaultValueSql("GETUTCDATE()");
      modelBuilder.Entity<InventoryRequest>().Property(i => i.RequestedKernels).HasDefaultValueSql("0");
      modelBuilder.Entity<InventoryRequest>()
        .HasOne(i => i.Inventory)
        .WithMany(ir => ir.InventoryRequests)
        .HasForeignKey(i => i.InventoryId);

      base.OnModelCreating(modelBuilder);
    }
  }
  public interface IDateTraceable
  {
    DateTime DateCreated { get; set; }
    DateTime DateUpdated { get; set; }
  }
}
