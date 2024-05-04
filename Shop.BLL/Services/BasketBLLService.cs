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

        public void DeleteFromBasket(Guid id, int reference)
        {
            _repo.DeleteFromBasket(id, reference);
        }

        public IEnumerable<BasketItemEntity> GetBasket(Guid id)
        {
            return _repo.GetBasket(id);
        }

        public void UpdateQuantity(Guid id, int reference, int quantity)
        {
            _repo.UpdateQuantity(id, reference, quantity);
        }
    }
}
