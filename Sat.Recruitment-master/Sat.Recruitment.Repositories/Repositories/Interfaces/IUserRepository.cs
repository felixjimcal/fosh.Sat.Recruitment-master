using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ResultModel> CreateUser(UserModel user);
    }
}