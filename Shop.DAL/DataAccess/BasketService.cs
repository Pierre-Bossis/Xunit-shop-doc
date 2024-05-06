using Dapper;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;
using System.Collections;
using System.Data.Common;

namespace Shop.DAL.DataAccess
{
    public class BasketService : IBasketRepository
    {
        private readonly DbConnection _connection;

        public BasketService(DbConnection connection)
        {
            _connection = connection;
        }
        public void AddToBasket(Guid id, int reference)
        {
            string sqlCheck = "SELECT COUNT(*) FROM [Basket] WHERE UserId = @id AND ArticleReference = @reference";
            int count = _connection.ExecuteScalar<int>(sqlCheck, new { id, reference });

            if (count > 0)
            {
                string sqlIncrement = "UPDATE [Basket] SET Quantity = Quantity + 1 WHERE UserId = @id AND ArticleReference = @reference";
                _connection.Execute(sqlIncrement, new { id, reference });
            }
            else
            {
                string sqlInsert = "INSERT INTO [Basket](UserId, ArticleReference) VALUES (@id, @reference)";
                _connection.Execute(sqlInsert, new { id, reference });
            }
        }

        public bool DeleteFromBasket(Guid id, int reference)
        {
            string sql = "DELETE FROM Basket WHERE UserId = @id AND ArticleReference = @reference";
            int rowsAffected =  _connection.Execute(sql, new { id, reference });

            return rowsAffected > 0;
        }

        //solution temporaire
        public IEnumerable<BasketItemEntity> GetBasket(Guid id)
        {
            string sql = "SELECT A.*,B.Quantity,B.DateAddedBasket FROM Article A INNER JOIN Basket B ON A.Reference = B.ArticleReference" +
                " WHERE B.UserId = @id";

            IEnumerable<ArticleEntity> articles = _connection.Query<ArticleEntity>(sql, new { id });

            IEnumerable<BasketItemEntity> basket = _connection.Query<BasketItemEntity>(sql, new { id });

            int i = 0;
            foreach (var item in basket)
            {
                item.Article = articles.ElementAt(i);
                i = i + 1;
            }

            return basket;
        }

        public bool UpdateQuantity(Guid id, int reference, string operation)
        {
            string sql = "";
            if (operation == "+")
                sql = "UPDATE [Basket] SET Quantity = Quantity + 1 WHERE UserId = @id AND ArticleReference = @reference";
            else
                sql = "UPDATE [Basket] SET Quantity = CASE WHEN Quantity > 1 THEN Quantity -1 ELSE 1 END WHERE UserId = @id AND ArticleReference = @reference";

            int rowsAffected = _connection.Execute(sql, new { id, reference });

            return rowsAffected > 0;
        }
    }
}
