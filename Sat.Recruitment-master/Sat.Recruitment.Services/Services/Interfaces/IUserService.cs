using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultModel> CreateUser(UserDto user);
    }
}