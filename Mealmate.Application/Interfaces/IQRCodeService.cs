using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IQRCodeService
    {
        Task<IEnumerable<QRCodeModel>> Get(int tableId);
        Task<QRCodeModel> GetById(int id);
        Task<QRCodeModel> Create(QRCodeCreateModel model);
        Task Update(QRCodeModel model);
        Task Delete(int id);
    }
}
