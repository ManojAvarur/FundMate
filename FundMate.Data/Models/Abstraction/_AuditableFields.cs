using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundMate.Data.Models.Abstraction;

public class _AuditableFields
{
    [Required]
    [ForeignKey(nameof(CreatedBy))]
    public Guid CreadtedById { get; set; }

    [Required]
    [ForeignKey(nameof(UpdatedBy))]
    public Guid UpdatedById { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public Users CreatedBy { get; set; } = null!;

    public Users UpdatedBy { get; set; } = null!;
}
