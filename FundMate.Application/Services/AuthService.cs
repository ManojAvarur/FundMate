using MapsterMapper;
using FundMate.Application.CustomExceptions;
using FundMate.Application.Dtos.Auth;
using FundMate.Application.Interfaces;
using FundMate.Data;
using FundMate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using FundMate.Application.Dtos;

namespace FundMate.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDataContext _dataContext;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthService(
        AppDataContext context, 
        ITokenService tokenService, 
        IPasswordHasherService passwordHasherService, 
        ILogger<AuthService> logger, 
        IMapper mapper,
        IConfiguration configuration
    ) { 
        _dataContext = context;
        _tokenService = tokenService;
        _passwordHasherService = passwordHasherService;
        _logger = logger;
        _mapper = mapper;
        _config = configuration;
    }

    /// <summary>
    /// Handles user login requests.
    /// </summary>
    /// <param name="loginRequestDto">The login request containing user credentials.</param>
    /// <returns>A LoginResponseDto containing the login result.</returns>
    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        try
        {
            _logger.LogInformation("Login attempt for {Email}", loginRequestDto.Email);
            var loginResponseDto = new LoginResponseDto();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);

            // Check if user exists and password is correct
            if (user == null || !user.IsVerified || !_passwordHasherService.VerifyPassword(user.Password, loginRequestDto.Password))
            {
                _logger.LogWarning("Failed login attempt for email {Email}", loginRequestDto.Email);

                loginResponseDto.ErrorMessage = $"Incorrect email or password";
                return loginResponseDto;
            }

            loginResponseDto.Token = _tokenService.GenerateUserLoggedInToken(user); // Generate JWT token

            _logger.LogInformation("User {UserId} - {Email} logged in successfully", user.Id, user.Email);
            
            return loginResponseDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login request failed");

            return new LoginResponseDto
            {
                ErrorMessage = "Login Request Failed!"
            };
        }
    }

    /// <summary>
    /// Handles user signup requests.
    /// </summary>
    /// <param name="userDto">The user details for signup.</param>
    /// <returns>A SimpleResponseDto containing the signup result.</returns>
    public async Task<SimpleResponseDto> Signup(UserDto userDto)
    {
        try
        {
            _logger.LogInformation("Signup attempt for {user}", userDto.Email);

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

            // If user exists but is not verified and OTP is expired, generate a new OTP
            var otpExpMins = _config.GetValue<int>("OTPExpirationMinutes");
            if(user != null)
            {
                if (user.IsVerified || !user.IsOtpExpired(otpExpMins))
                {
                    _logger.LogInformation("User already exists {user}", userDto.Email);
                    return new SimpleResponseDto("User already exists", HttpStatusCode.Forbidden);
                }

                _dataContext.Users.Remove(user);
            }

            if (!userDto.VerifyPassword())
            {
                return new SimpleResponseDto("Password does not meet complexity requirements", HttpStatusCode.BadRequest);
            }

            var newUser = _mapper.Map<Users>(userDto);
            newUser.Password = _passwordHasherService.HashPassword(userDto.Password);
            newUser.GenerateNewOtp();
            _dataContext.Add(newUser);
            await _dataContext.SaveChangesAsync();

            // TODO: Send an email
            
            return new SimpleResponseDto(successStatus: true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Singup request failed for {user}!", userDto.Email);
            return new SimpleResponseDto("Signup Request Failed!", HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Handles user signup OTP verification requests.
    /// </summary>
    /// <param name="userDto">The user details containing the OTP.</param>
    /// <returns>A SimpleResponseDto containing the OTP verification result.</returns>
    public async Task<SimpleResponseDto> SignupOTPVerification(OtpVerificationRequestDto otpVerificationRequestDto)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == otpVerificationRequestDto.Email);

        if(user == null || user.IsVerified)
        {
            _logger.LogInformation("User not found {user}", otpVerificationRequestDto.Email);
            return new SimpleResponseDto("User not found!", HttpStatusCode.BadRequest);
        }

        if (!VerifyOTP(user, otpVerificationRequestDto.Otp))
        {
            _logger.LogInformation("Invalid OTP for {user}", otpVerificationRequestDto.Email);
            return new SimpleResponseDto("Invalid OTP!", HttpStatusCode.BadRequest);
        }

        user.IsVerified = true;
        user.ClearOTP();
        await _dataContext.SaveChangesAsync();

        return new SimpleResponseDto(successStatus: true);
    }

    // TODO: Forgot Password Implementation

    // TODO: Resend OTP Implementation

    /// <summary>
    /// Verifies the provided OTP for the given user.
    /// </summary>
    /// <param name="user">The user for whom the OTP is being verified.</param>
    /// <param name="otp">The OTP to verify.</param>
    /// <returns>True if the OTP is valid and not expired; otherwise, false.</returns>
    private bool VerifyOTP(Users user, string otp)
    {
        _logger.LogInformation("OTP verification for {user}", user.Email);

        var otpExpMins = _config.GetValue<int>("OTPExpirationMinutes");
        return !user.IsOtpExpired(otpExpMins) && SlowEquals(user.OtpCode!, otp);
    }

    /// <summary>
    /// Compares two strings in a way that mitigates timing attacks.
    /// </summary>
    /// <param name="a">The first string to compare.</param>
    /// <param name="b">The second string to compare.</param>
    /// <returns>True if the strings are equal; otherwise, false.</returns>
    private bool SlowEquals(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        var diff = 0;
        for (var i = 0; i < a.Length; i++)
            diff |= a[i] ^ b[i];
        return diff == 0;
    }
}
