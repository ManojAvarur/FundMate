using FundMate.Application.Dtos;
using FundMate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;

namespace FundMate.Application.Mappings;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, Users>()
            .Ignore(dest => dest.Password);

        config.NewConfig<Users, UserDto>();
    }
}
