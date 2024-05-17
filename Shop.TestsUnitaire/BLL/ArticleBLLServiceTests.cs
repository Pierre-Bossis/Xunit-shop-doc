using Moq;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;

namespace Shop.TestsUnitaire.BLL
{
    public class ArticleBLLServiceTests
    {
        #region GetAll Tests
        [Fact]
        public void GetAllTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            List<ArticleEntity> list = [];

            mockArticleRepository.Setup(repo => repo.GetAll()).Returns(list);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            IEnumerable<ArticleEntity> entities = repository.GetAll();

            Assert.NotNull(entities);
            Assert.IsAssignableFrom<IEnumerable<ArticleEntity>>(entities);
        }
        #endregion

        #region GetByReference Tests
        [Fact]
        public void GetByReferenceValidTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.GetByReference(1)).Returns(new ArticleEntity() { Reference = 5});

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            ArticleEntity entity = repository.GetByReference(1);

            Assert.NotNull(entity);
            Assert.NotNull(entity.Reference);
            Assert.IsType<ArticleEntity>(entity);
        }

        [Fact]
        public void GetByReferenceInvalidTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.GetByReference(It.IsAny<int>())).Returns((ArticleEntity)null);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            ArticleEntity entity = repository.GetByReference(1);

            Assert.Null(entity);
        }
        #endregion
    }
}
