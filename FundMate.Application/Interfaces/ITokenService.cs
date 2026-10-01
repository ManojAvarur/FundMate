using FundMate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundMate.Application.Interfaces;

public interface ITokenService
{
    string GenerateUserLoggedInToken(Users user);

    string GenerateUserSignupToken(Users user)  ;
}
