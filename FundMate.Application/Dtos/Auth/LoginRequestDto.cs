using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundMate.Application.Dtos.Auth;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email!")]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
