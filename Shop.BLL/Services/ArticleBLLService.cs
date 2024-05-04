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
    public class ArticleBLLService : IArticleBLLRepository
    {
        private readonly IArticleRepository _repo;

        public ArticleBLLService(IArticleRepository repo)
        {
            _repo = repo;
        }

        public bool Create(ArticleEntity article)
        {
            return _repo.Create(article);
        }

        public bool DeleteByReference(int reference)
        {
            return _repo.DeleteByReference(reference);
        }

        public IEnumerable<ArticleEntity> GetAll()
        {
            return _repo.GetAll();
        }

        public ArticleEntity GetByReference(int reference)
        {
            return _repo.GetByReference(reference);
        }

        public IEnumerable<ArticleEntity> Search(string nom)
        {
            return _repo.Search(nom);
        }

        public bool Update(ArticleEntity article)
        {
            return _repo.Update(article);
        }
    }
}
