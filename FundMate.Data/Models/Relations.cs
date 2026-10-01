using FundMate.Data.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundMate.Data.Models;

public class Relations : _AuditableFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(UserOne))]
    public Guid UserIdOne { get; set; }

    [Required]
    [ForeignKey(nameof(UserTwo))]
    public Guid UserIdTwo { get; set; }

    // Navigation properties
    public virtual Users UserOne { get; set; } = null!;

    public virtual Users UserTwo { get; set; } = null!;
}
