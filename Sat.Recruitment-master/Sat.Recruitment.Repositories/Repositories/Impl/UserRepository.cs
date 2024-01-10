using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repositories.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private List<UserModel> _users;

        public UserRepository()
        {
            _users = new List<UserModel>() {
                new() { Name = "Atlas", Email = "atlas@gmail.com", Address = "calle falsa 123", Money = 10, Phone = "123", UserType = Models.Enums.UserType.Normal },
                new() { Name = "Ginebra", Email = "ginebra@gmail.com", Address = "Wallaby 42, Sydney", Money = 101, Phone = "456", UserType = Models.Enums.UserType.SuperUser },
                new() { Name = "Arwen", Email = "arwen@gmail.com", Address = "10880 Malibu Point, 90265", Money = 200, Phone = "789", UserType = Models.Enums.UserType.Premium }
            };
        }

        public Task<ResultModel> CreateUser(UserModel newUser)
        {
            ResultModel result = new() { IsSuccess = false, Errors = "User is duplicated" };

            var isDuplicated = _users.Any(u => u.Email == newUser.Email || u.Phone == newUser.Phone || (u.Name == newUser.Name && u.Address == newUser.Address));
            if (!isDuplicated)
            {
                _users.Add(newUser);

                result.IsSuccess = true;
                result.Errors = "User Created";
            }

            return Task.FromResult(result);
        }
    }
}