using FundMate.Data.Extensions;
using FundMate.Data.Models;
using FundMate.Data.Models.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FundMate.Data;

public class AppDataContext : DbContext
{
    public DbSet<Users> Users { get; set; }

    public DbSet<Relations> Relations { get; set; }

    public DbSet<Groups> Groups { get; set; }

    public DbSet<GroupUsers> GroupUsers { get; set; }

    public DbSet<GroupTransactions> GroupTransactions { get; set; }

    public DbSet<SplitType> SplitTypes { get; set; }

    public DbSet<Settlements> Settlements { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Restrict CreatedBy/UpdatedBy delete cascades so removing a user
        // never destroys every row they created or last touched.
        modelBuilder.Entity<Groups>().HasAuditableRelationships();
        modelBuilder.Entity<GroupUsers>().HasAuditableRelationships();
        modelBuilder.Entity<GroupTransactions>().HasAuditableRelationships();
        modelBuilder.Entity<Relations>().HasAuditableRelationships();
        modelBuilder.Entity<SplitType>().HasAuditableRelationships();
        modelBuilder.Entity<TransactionSubscribers>().HasAuditableRelationships();
        modelBuilder.Entity<Settlements>().HasAuditableRelationships();

        modelBuilder.Entity<Settlements>(entity =>
        {
            // Settlements are group-scoped payments between two group members.
            // Deleting a group cascades its settlements, but deleting a
            // GroupUsers row (a membership) must not silently erase the
            // payment history — restrict instead.
            entity.HasOne(s => s.Group)
                .WithMany()
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Payer)
                .WithMany()
                .HasForeignKey(s => s.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Payee)
                .WithMany()
                .HasForeignKey(s => s.PayeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(s => s.GroupId);
            entity.HasIndex(s => s.PayerId);
            entity.HasIndex(s => s.PayeeId);

            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Settlements_PayerPayeeDiffer",
                $"\"{nameof(Models.Settlements.PayerId)}\" <> \"{nameof(Models.Settlements.PayeeId)}\""));
        });
    }

    /// <summary>
    /// Automatically populates auditable fields before saving changes.
    /// Pass the current user's ID via <paramref name="currentUserId"/>.
    /// </summary>
    public Task<int> SaveChangesAsync(Guid currentUserId, CancellationToken cancellationToken = default)
    {
        ApplyAuditFields(currentUserId);
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditFields(Guid currentUserId)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<_AuditableFields>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedById = currentUserId;
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedById = currentUserId;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                // Preserve original insert values
                entry.Property(e => e.CreatedById).IsModified = false;
                entry.Property(e => e.CreatedAt).IsModified = false;

                entry.Entity.UpdatedById = currentUserId;
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}
