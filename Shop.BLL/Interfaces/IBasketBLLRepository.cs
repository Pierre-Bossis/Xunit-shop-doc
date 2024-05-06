using Shop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IBasketBLLRepository
    {
        void AddToBasket(Guid id, int reference);
        IEnumerable<BasketItemEntity> GetBasket(Guid id);
        bool DeleteFromBasket(Guid id, int reference);
        bool UpdateQuantity(Guid id, int reference, string operation);
    }
}
