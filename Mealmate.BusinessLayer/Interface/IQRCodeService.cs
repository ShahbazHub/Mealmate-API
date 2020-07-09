// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IQRCodeService
    {
        IEnumerable<QRCode> Get();
        QRCode GetById(int id);
        int Create(QRCode model);
        int Update(int id, QRCode model);
        bool Delete(int id);
    }
}
