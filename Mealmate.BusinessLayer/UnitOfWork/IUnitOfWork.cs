using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
