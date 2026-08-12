using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FundMate.Data.Models.Abstraction;

namespace FundMate.Data.Models;

public class Groups : _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }
}