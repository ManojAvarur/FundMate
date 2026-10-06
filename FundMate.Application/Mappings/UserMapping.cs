using FundMate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using FundMate.Application.Dtos.Auth;

namespace FundMate.Application.Mappings;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, Users>()
            .Ignore(dest => dest.Password)
            .Ignore(dest => dest.OtpCode);

        config.NewConfig<Users, UserDto>()
            .Ignore(dest => dest.Password)
            .Ignore(dest => dest.Otp);
    }
}
