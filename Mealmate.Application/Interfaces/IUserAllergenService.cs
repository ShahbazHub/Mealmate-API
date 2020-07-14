using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserAllergenService
    {
        Task<IEnumerable<UserAllergenModel>> Get(int UserId);
        Task<UserAllergenModel> GetById(int id);
        Task<UserAllergenModel> Create(UserAllergenModel model);
        Task Update(UserAllergenModel model);
        Task Delete(int id);
    }
}
