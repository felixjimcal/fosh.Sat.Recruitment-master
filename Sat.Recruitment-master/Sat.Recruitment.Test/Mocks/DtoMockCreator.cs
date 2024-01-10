using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Sat.Recruitment.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Mocks
{
    public static class DtoMockCreator
    {
        public static UserDto CreateUserDto(string? name, string? address, string? email, string? phone, int money, int userType) => new()
        {
            Name = name,
            Address = address,
            Email = email,
            Phone = phone,
            Money = money,
            UserType = userType
        };
    }
}