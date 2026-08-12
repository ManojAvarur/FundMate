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

    public Users Users { get; set; } = null!;

    public Groups Groups { get; set; } = null!;
}