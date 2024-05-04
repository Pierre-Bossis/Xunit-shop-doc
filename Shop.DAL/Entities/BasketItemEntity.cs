using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Entities
{
    public class BasketItemEntity
    {
        public ArticleEntity Article { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAddedBasket { get; set; }
    }
}
