using System.ComponentModel.DataAnnotations;

namespace FundMate.Application.Dtos.Auth;

public class OtpVerificationRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Otp { get; set; } = null!;
}
