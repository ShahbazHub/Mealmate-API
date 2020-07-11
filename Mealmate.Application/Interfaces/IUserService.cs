using Mealmate.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> Get();
        Task<UserModel> GetById(int id);
        Task<UserModel> Create(UserModel model);
        Task Update(UserModel model);
        Task Delete(int id);
    }
}
