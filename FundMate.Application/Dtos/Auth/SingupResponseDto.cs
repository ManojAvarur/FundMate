using System;
using System.Collections.Generic;
using System.Text;

namespace FundMate.Application.Dtos.Auth;

public class SingupResponseDto
{
    public bool Success { get; set; }

    public string? ErrorMessage { get; set; }
}
