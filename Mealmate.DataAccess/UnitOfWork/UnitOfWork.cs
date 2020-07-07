using Mealmate.DataAccess.Entities.Mealmate;
using Mealmate.DataAccess.Repositories;
using System;
using System.Diagnostics;

namespace Mealmate.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealmateDbContext _context;
        private Repository<Restaurant> _restaurantRepository;

        public UnitOfWork(MealmateDbContext context)
        {
            _context = context;
        }

        public Repository<Restaurant> RestaurantRepository
        {
            get
            {
                if (_restaurantRepository == null)
                {
                    _restaurantRepository = new Repository<Restaurant>(_context);
                }

                return _restaurantRepository;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        #region IDiosposable
        private bool disposed = false;

        /// <summary>  
        /// Protected Virtual Dispose method  
        /// </summary>  
        /// <param name="disposing"></param>  
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("Object for UoW is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>  
        /// Dispose method  
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
