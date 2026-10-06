using FundMate.Application.Dtos;
using FundMate.Application.Dtos.Auth;
using FundMate.Application.Interfaces;
using FundMate.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FundMate.Web.Controllers;

/// <summary>
/// Controller responsible for handling authentication-related actions
/// </summary>
public class AuthController : _V1BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
    { 
        _authService = authService;
    }

    /// <summary>
    /// Handles user login requests.
    /// </summary>
    /// <param name="loginRequestDto">The login request containing user credentials.</param>
    /// <returns>An ActionResult containing the login response.</returns>
    [HttpPost("[action]")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
    {
        var result = await _authService.Login(loginRequestDto);

        if(result.ErrorMessage != null)
            return Unauthorized(result);

        return Ok(result);
    }

    /// <summary>
    /// Handles user signup requests.
    /// </summary>
    /// <param name="users">The user details for signup.</param>
    /// <returns>An ActionResult containing the signup response.</returns>
    [HttpPost("[action]")]
    public async Task<ActionResult<SimpleResponseDto>> Signup(UserDto users)
    {
        var result = await _authService.Signup(users);
        return ProcessSimpleResponseDto(result);
    }

    /// <summary>
    /// Handles user signup OTP verification requests.
    /// </summary>
    /// <param name="otpVerificationRequestDto">The user details containing the OTP.</param>
    /// <returns>An ActionResult containing the OTP verification response.</returns>
    [HttpPost("signup-otp-verification")]
    public async Task<ActionResult<SimpleResponseDto>> SignupOtpVerification(OtpVerificationRequestDto otpVerificationRequestDto)
    {
        var result = await _authService.SignupOTPVerification(otpVerificationRequestDto);
        return ProcessSimpleResponseDto(result);
    }
}
