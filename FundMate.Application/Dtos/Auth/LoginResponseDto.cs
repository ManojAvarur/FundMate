using System;
using System.Collections.Generic;
using System.Text;

namespace FundMate.Application.Dtos.Auth;

public class LoginResponseDto
{
    public string? ErrorMessage { get; set; }

    public string? Token { get; set; }
}
