using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserDietaryService
    {
        Task<IEnumerable<UserDietaryModel>> Get(int UserId);
        Task<UserDietaryModel> GetById(int id);
        Task<UserDietaryModel> Create(UserDietaryModel model);
        Task Update(UserDietaryModel model);
        Task Delete(int id);
    }
}
