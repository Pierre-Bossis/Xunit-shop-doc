using Shop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
    public interface IBasketRepository
    {
        void AddToBasket(Guid id,int reference);
        IEnumerable<BasketItemEntity> GetBasket(Guid id);
        bool DeleteFromBasket(Guid id,int reference);
        bool UpdateQuantity(Guid id, int reference, string operation);

    }
}
