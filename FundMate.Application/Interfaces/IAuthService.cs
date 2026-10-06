using FundMate.Application.Dtos;
using FundMate.Application.Dtos.Auth;
using FundMate.Data.Models;

namespace FundMate.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

    Task<SimpleResponseDto> Signup(UserDto user);

    Task<SimpleResponseDto> SignupOTPVerification(OtpVerificationRequestDto otpVerificationRequestDto);
}
