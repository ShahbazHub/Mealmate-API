using Mealmate.BusinessLayer.Interface;
using Mealmate.DataAccess;
using Mealmate.DataAccess.Entities.Mealmate;
using Mealmate.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Mealmate.BusinessLayer.Common
{
    public class RestaurantService : IRestaurantService
    {
        private readonly UnitOfWork _unitOfWork;

        public RestaurantService(MealmateDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public int Create(Restaurant model)
        {
            using (var scope = new TransactionScope())
            {
                model.Created = DateTime.UtcNow;
                _unitOfWork.RestaurantRepository.Insert(model);
                _unitOfWork.Complete();
                scope.Complete();
                return model.RestaurantId;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var temp = _unitOfWork.RestaurantRepository.GetById(id);
                if (temp != null)
                {
                    _unitOfWork.RestaurantRepository.Delete(temp);
                    _unitOfWork.Complete();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public IEnumerable<Restaurant> Get()
        {
            return _unitOfWork.RestaurantRepository.Get();
        }

        public Restaurant GetById(int id)
        {
            return _unitOfWork.RestaurantRepository.GetById(id);
        }

        public int Update(int id, Restaurant model)
        {
            if (model != null)
            {
                using (var scope = new TransactionScope())
                {
                    var temp = _unitOfWork.RestaurantRepository.GetById(id);
                    if (temp != null)
                    {
                        temp.Created = model.Created;
                        temp.Description = model.Description;
                        _unitOfWork.RestaurantRepository.Update(temp);
                        _unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }

            return id;
        }
    }
}
