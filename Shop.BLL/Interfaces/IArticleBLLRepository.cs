using Shop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IArticleBLLRepository
    {
        IEnumerable<ArticleEntity> GetAll();
        ArticleEntity GetByReference(int reference);
        bool Update(ArticleEntity article);
        bool Create(ArticleEntity article);
        bool DeleteByReference(int reference);
        IEnumerable<ArticleEntity> Search(string nom);
    }
}
