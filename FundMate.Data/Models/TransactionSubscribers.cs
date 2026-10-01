using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FundMate.Data.Models.Abstraction;

namespace FundMate.Data.Models;

public class TransactionSubscribers : _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(GroupTransaction))]
    public int GroupTransactionId { get; set; }

    [Required]
    [ForeignKey(nameof(GroupUser))]
    public int GroupUserId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal SplitValue { get; set; }

    [Required, Range(0, 999_999)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal OwedAmount { get; set; }

    public bool IsPaid { get; set; } = false;

    // Navigation properties
    public virtual GroupTransactions GroupTransaction { get; set; } = null!;

    public virtual GroupUsers GroupUser { get; set; } = null!;
}
