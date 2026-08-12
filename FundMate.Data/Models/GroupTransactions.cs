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
    [ForeignKey(nameof(Users))]
    public Guid PaidById { get; set; }

    [Required]
    [MaxLength(150)]
    public string PaymentDescription { get; set; } = null!;
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    [Required, Range(1, 999_999)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [Required]
    [ForeignKey(nameof(RecipientGroup))]
    public int RecipientGroupId { get; set; }

    public Users Users { get; set; } = null!;

    public Groups RecipientGroup { get; set; } = null!;
}