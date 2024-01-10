using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Enums;
using Sat.Recruitment.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Mappings
{
    public static class UserMapping
    {
        public static UserModel ToModel(this UserDto dto)
        {
            UserModel userModel = new()
            {
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                Name = dto.Name,
                Money = dto.Money,
                UserType = (UserType)dto.UserType
            };

            decimal parsedMoney = userModel.Money;
            decimal gif = 0;

            if (parsedMoney > 100)
            {
                switch (userModel.UserType)
                {
                    case UserType.Normal:
                        gif = parsedMoney * 0.12m;
                        break;

                    case UserType.SuperUser:
                        gif = parsedMoney * 0.20m;
                        break;

                    case UserType.Premium:
                        gif = parsedMoney * 2;
                        break;
                }
            }
            else if (parsedMoney > 10 && userModel.UserType == UserType.Normal)
            {
                gif = parsedMoney * 0.08m;
            }

            userModel.Money += gif;

            return userModel;
        }
    }
}