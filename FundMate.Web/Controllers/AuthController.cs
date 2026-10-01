using FundMate.Application.Dtos;
using FundMate.Application.Dtos.Auth;
using FundMate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FundMate.Web.Controllers;

public class AuthController : _V1BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
    { 
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
    {
        var result = await _authService.Login(loginRequestDto);

        if(result.ErrorMessage != null)
            return Unauthorized(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SingupResponseDto>> Signup(UserDto users)
    {
        var result = await _authService.Signup(users);

        return result;
    }
}
