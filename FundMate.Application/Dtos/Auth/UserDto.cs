using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FundMate.Application.Dtos.Auth;

public class UserDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must contain a valid domain (e.g. user@example.com).")]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [JsonIgnore]
    public string? Otp { get; set; }

    public bool VerifyPassword()
    {
        if (Password.Length < 10
            || !Password.Any(char.IsUpper)
            || !Password.Any(char.IsLower)
            || !Password.Any(char.IsDigit)
            || (!Password.Any(char.IsSymbol) && !Password.Any(char.IsPunctuation))
        ) {
            return false;
        }

        return true;
    }
}
