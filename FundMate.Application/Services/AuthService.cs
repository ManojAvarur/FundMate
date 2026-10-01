using AutoMapper;
using FundMate.Application.CustomExceptions;
using FundMate.Application.Dtos;
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

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        try
        {
            _logger.LogInformation("Login attempt for {Email}", loginRequestDto.Email);
            var loginResponseDto = new LoginResponseDto();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);

            if (user == null || !_passwordHasherService.VerifyPassword(user.Password, loginRequestDto.Password))
            {
                _logger.LogWarning("Failed login attempt for email {Email}", loginRequestDto.Email);

                loginResponseDto.ErrorMessage = $"Incorrect email or password";
                return loginResponseDto;
            }

            loginResponseDto.Token = _tokenService.GenerateUserLoggedInToken(user);

            _logger.LogInformation("User {UserId} - {Email} logged in successfully", user.Id, user.Email);
            
            return loginResponseDto;
        } 
        catch(InputValidationException ex)
        {
            _logger.LogInformation("Login attempt for {Email}", loginRequestDto.Email);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login request failed");

            var loginResponseDto = new LoginResponseDto();
            loginResponseDto.ErrorMessage = "Login Request Failed!";
            return loginResponseDto;
        }
    }

    public async Task<SingupResponseDto> Signup(UserDto userDto)
    {
        var response = new SingupResponseDto();
        try
        {
            _logger.LogInformation("Signup attempt for {user}", userDto);

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

            var otpExpMins = _config.GetValue<int>("OTPExpirationMinutes");
            if(user != null && (user.IsVerified || !user.IsOtpExpired(otpExpMins)))
            {
                _logger.LogInformation("User already exists {user}", userDto);
                response.ErrorMessage = "User already exists";
                return response;
            }

            var newUser = _mapper.Map<Users>(userDto);
            newUser.GenerateNewOtp();
            await _dataContext.SaveChangesAsync();
            
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Singup request failed for {user}!", userDto);
            response.ErrorMessage = "Signup Request Failed!";
            return response;
        }
    }

    public Task<SingupResponseDto> OtpVerification(string email, string otp)
    {
        throw new NotImplementedException();
    }
}
