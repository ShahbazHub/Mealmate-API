// using Mealmate.DataAccess.Entities.Mealmate;
using Mealmate.DataAccess.Entities.Lookup;
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
        private Repository<Branch> _branchRepository;
        private Repository<Location> _locationRepository;
        private Repository<Table> _tableRepository;
        private Repository<Menu> _menuRepository;
        private Repository<MenuItem> _menuItemRepository;
        private Repository<MenuItemOption> _menuItemOptionRepository;
        private Repository<OptionItem> _optionItemRepository;

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

        public Repository<Branch> BranchRepository
        {
            get
            {
                if (_branchRepository == null)
                {
                    _branchRepository = new Repository<Branch>(_context);
                }

                return _branchRepository;
            }
        }

        public Repository<Location> LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new Repository<Location>(_context);
                }

                return _locationRepository;
            }
        }

        public Repository<Table> TableRepository
        {
            get
            {
                if (_tableRepository == null)
                {
                    _tableRepository = new Repository<Table>(_context);
                }

                return _tableRepository;
            }
        }

        public Repository<Menu> MenuRepository
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new Repository<Menu>(_context);
                }

                return _menuRepository;
            }
        }

        public Repository<MenuItem> MenuItemRepository
        {
            get
            {
                if (_menuItemRepository == null)
                {
                    _menuItemRepository = new Repository<MenuItem>(_context);
                }

                return _menuItemRepository;
            }
        }

        public Repository<MenuItemOption> MenuItemOptionRepository
        {
            get
            {
                if (_menuItemOptionRepository == null)
                {
                    _menuItemOptionRepository = new Repository<MenuItemOption>(_context);
                }

                return _menuItemOptionRepository;
            }
        }

        public Repository<OptionItem> OptionItemRepository
        {
            get
            {
                if (_optionItemRepository == null)
                {
                    _optionItemRepository = new Repository<OptionItem>(_context);
                }

                return _optionItemRepository;
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
