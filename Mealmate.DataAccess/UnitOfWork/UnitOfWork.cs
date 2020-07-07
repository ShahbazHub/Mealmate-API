using Mealmate.DataAccess.Contexts;
using Mealmate.Entities.Infrastructure;
using System;
using System.Diagnostics;

namespace Mealmate.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly MealmateDbContext _context;

        //private RestaurantRepository<Restaurant> _restaurantRepository;

        public UnitOfWork(MealmateDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    //_context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
