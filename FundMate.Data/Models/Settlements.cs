using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FundMate.Data.Models.Abstraction;

namespace FundMate.Data.Models;

/// <summary>
/// Records an actual payment made between two group members to clear
/// (fully or partially) an outstanding balance. This is an immutable
/// ledger entry — it should never be edited or deleted after creation,
/// only ever inserted.
/// </summary>
public class Settlements : _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }

    [Required]
    [ForeignKey(nameof(Payer))]
    public int PayerId { get; set; }

    [Required]
    [ForeignKey(nameof(Payee))]
    public int PayeeId { get; set; }

    [Required, Range(0.01, 999_999)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    // Navigation properties
    public virtual Groups Group { get; set; } = null!;

    public virtual GroupUsers Payer { get; set; } = null!;

    public virtual GroupUsers Payee { get; set; } = null!;
}
