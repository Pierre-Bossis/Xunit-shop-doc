using Shop.DAL.Entities;
using Shop.Models;
using Shop.Tools.Mappers.Article;
using static Dapper.SqlMapper;

namespace Shop.Tools.Mappers
{
    public static class BasketMapper
    {
        public static BasketItemDTO ToDto(this BasketItemEntity e)
        {
            if (e is null) return null;

            var dto = new BasketItemDTO()
            {
                Article = e.Article.ToDtoGET(),
                Quantity = e.Quantity,
                DateAddedBasket = e.DateAddedBasket
            };

            return dto;
        }
    }
}
