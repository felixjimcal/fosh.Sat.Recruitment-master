using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Repositories.Repositories.Interfaces;
using Sat.Recruitment.Services.Mappings;
using Sat.Recruitment.Services.Services.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultModel> CreateUser(UserDto userDto)
        {
            var user = userDto.ToModel();
            var result = await _userRepository.CreateUser(user);
            return result;
        }
    }
}