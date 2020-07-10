using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IQRCodeService
    {
        Task<IEnumerable<QRCodeModel>> Get(int tableId);
        Task<QRCodeModel> GetById(int id);
        Task<int> Create(QRCodeModel model);
        Task<int> Update(int id, QRCodeModel model);
        Task<bool> Delete(int id);
    }
}
