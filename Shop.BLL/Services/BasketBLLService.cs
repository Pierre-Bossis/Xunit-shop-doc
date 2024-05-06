using Shop.BLL.Interfaces;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
    public class BasketBLLService : IBasketBLLRepository
    {
        private readonly IBasketRepository _repo;

        public BasketBLLService(IBasketRepository repo)
        {
            _repo = repo;   
        }
        public void AddToBasket(Guid id, int reference)
        {
            _repo.AddToBasket(id, reference);
        }

        public bool DeleteFromBasket(Guid id, int reference)
        {
            return _repo.DeleteFromBasket(id, reference);
        }

        public IEnumerable<BasketItemEntity> GetBasket(Guid id)
        {
            return _repo.GetBasket(id);
        }

        public bool UpdateQuantity(Guid id, int reference, string operation)
        {
            return _repo.UpdateQuantity(id, reference, operation);
        }
    }
}
