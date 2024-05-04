using Shop.Models.Article;

namespace Shop.Models
{
    public class BasketItemDTO
    {
        public ArticleDTO Article { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAddedBasket { get; set; }
    }
}
