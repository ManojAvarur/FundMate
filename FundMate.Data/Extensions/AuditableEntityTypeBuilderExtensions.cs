using FundMate.Data.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundMate.Data.Extensions;

public static class AuditableEntityTypeBuilderExtensions
{
    /// <summary>
    /// Configures the CreatedBy/UpdatedBy audit relationships for an entity
    /// deriving from <see cref="_AuditableFields"/> so that deleting a
    /// <see cref="Models.Users"/> row is restricted rather than cascading
    /// into every row that user created or last updated.
    /// </summary>
    public static EntityTypeBuilder<TEntity> HasAuditableRelationships<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : _AuditableFields
    {
        builder
            .HasOne(e => e.CreatedBy)
            .WithMany()
            .HasForeignKey(e => e.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.UpdatedBy)
            .WithMany()
            .HasForeignKey(e => e.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        return builder;
    }
}
