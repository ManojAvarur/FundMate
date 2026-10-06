using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FundMate.Data.Models;

[Index(nameof(Email), IsUnique = true)]
public class Users
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public bool IsSuperAdmin { get; set; } = false;

    [Required]
    public bool IsVerified { get; set; } = false;

    [MaxLength(6)]
    public string? OtpCode { get; set; }

    public DateTime? OtpCreatedAt { get; set; }

    public virtual bool IsOtpExpired(int otpExpirationMinutes)
    {
        if (string.IsNullOrWhiteSpace(OtpCode) || OtpCreatedAt is null)
        {
            return true;
        }

        return OtpCreatedAt.Value.AddMinutes(otpExpirationMinutes) <= DateTime.UtcNow;
    }

    public virtual void GenerateNewOtp()
    {
        OtpCode = RandomNumberGenerator.GetHexString(6);
        OtpCreatedAt = DateTime.UtcNow;
    }

    public void ClearOTP()
    {
        OtpCode = null;
        OtpCreatedAt = null;
    }
}