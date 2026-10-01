using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FundMate.Data.Models.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FundMate.Data.Models;

public class GroupTransactions: _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(GroupUsers))]
    public int PaidById { get; set; }

    [Required]
    [ForeignKey(nameof(SplitType))]
    public int SplitTypeId { get; set; }

    [Required]
    [MaxLength(150)]
    public string PaymentDescription { get; set; } = null!;
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    [Required, Range(1, 999_999)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    // Navigation properties
    public virtual GroupUsers GroupUsers { get; set; } = null!;

    public virtual SplitType SplitType { get; set; } = null!;
}