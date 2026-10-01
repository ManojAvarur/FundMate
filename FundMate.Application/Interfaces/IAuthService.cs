using FundMate.Application.Dtos;
using FundMate.Application.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundMate.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

    Task<SingupResponseDto> Signup(UserDto user);

    Task<SingupResponseDto> OtpVerification(string email, string otp);
}
