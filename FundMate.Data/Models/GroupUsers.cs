using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FundMate.Data.Models;
using FundMate.Data.Models.Abstraction;

namespace FundMate.Data.Models;

public class GroupUsers : _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Users))]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey(nameof(Groups))]
    public int GroupId { get; set; }

    // Navigation properties
    public virtual Users Users { get; set; } = null!;

    public virtual Groups Groups { get; set; } = null!;
}