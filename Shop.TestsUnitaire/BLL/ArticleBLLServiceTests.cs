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
        public void GetAll_Test()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            List<ArticleEntity> list = [];

            mockArticleRepository.Setup(repo => repo.GetAll()).Returns(list);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            IEnumerable<ArticleEntity> entities = repository.GetAll();

            Assert.NotNull(entities);
            mockArticleRepository.Verify(repo => repo.GetAll(), Times.Once);
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
            mockArticleRepository.Verify(repo => repo.GetByReference(1), Times.Once);
            Assert.IsType<ArticleEntity>(entity);
        }

        [Fact]
        public void GetByReferenceInvalidTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.GetByReference(It.IsAny<int>())).Returns((ArticleEntity)null);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            ArticleEntity entity = repository.GetByReference(1);
            mockArticleRepository.Verify(repo => repo.GetByReference(It.IsAny<int>()), Times.Once);
            Assert.Null(entity);
        }
        #endregion

        #region DeleteByReference Test

        [Fact]
        public void DeleteByReferenceValidTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            int referenceToDelete = 1;
            string expectedPath = "ImageToDeletePath";

            mockArticleRepository.Setup(repo => repo.DeleteByReference(referenceToDelete)).Returns(expectedPath);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            string actualPath = repository.DeleteByReference(referenceToDelete);

            Assert.NotNull(actualPath);
            Assert.Equal(expectedPath, actualPath);
            mockArticleRepository.Verify(repo => repo.DeleteByReference(referenceToDelete), Times.Once);

        }

        [Fact]
        public void DeleteByReferenceInvalidTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            int referenceToDelete = 60;
            string expectedPath = "ImageToDeletePath";

            mockArticleRepository.Setup(repo => repo.DeleteByReference(referenceToDelete)).Returns((string)null);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            string actualPath = repository.DeleteByReference(referenceToDelete);

            Assert.Null(actualPath);
            mockArticleRepository.Verify(repo => repo.DeleteByReference(referenceToDelete), Times.Once);
        }

        #endregion

        #region Search Test

        [Fact]
        public void SearchMore0ResultTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            string searchValue = "nom article";
            List<ArticleEntity> entities = new List<ArticleEntity> { new(), new() };
            mockArticleRepository.Setup(repo => repo.Search(searchValue)).Returns(entities);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            IEnumerable<ArticleEntity> articles = repository.Search(searchValue);

            mockArticleRepository.Verify(repo => repo.Search(searchValue), Times.Once);
            Assert.NotNull(articles);
        }

        [Fact]
        public void Search0ResultTest()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            string searchValue = "nom article";
            List<ArticleEntity> entities = new List<ArticleEntity>();
            mockArticleRepository.Setup(repo => repo.Search(searchValue)).Returns(entities);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            IEnumerable<ArticleEntity> articles = repository.Search(searchValue);
            mockArticleRepository.Verify(repo => repo.Search(searchValue), Times.Once);
            Assert.NotNull(articles);
        }

        #endregion

        #region Update Test
        [Fact]
        public void Update_Article_Return_True()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();

            ArticleEntity entity = new ArticleEntity()
            {
                Reference = 550,
                Nom = "nom",
                Description = "description",
                Categorie = "categorie",
                Quantite = 3,
                Image = "path",
                Prix = 50.25m,
                QuantiteVendue = 0,
                NombreRecommandations = 0,
                Poids = 0,
                Taille = 0,
                Provenance = "Chine",
                DateMiseEnLigne = new DateTime(2024,05,29),
                Fournisseur = "Fournisseur",
                MotsCles = "motscles1,motscles2",
            };

            mockArticleRepository.Setup(repo => repo.Update(entity)).Returns(true);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            bool success = repository.Update(entity);

            Assert.True(success);
            mockArticleRepository.Verify(repo => repo.Update(entity), Times.Once);
        }

        [Fact]
        public void Update_Article_Return_False()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();

            ArticleEntity entity = new ArticleEntity()
            {
                Reference = 0,
                Nom = "nom",
                Description = "description",
                Categorie = "categorie",
                Quantite = 3,
                Image = "path",
                Prix = 50.25m,
                QuantiteVendue = 0,
                NombreRecommandations = 0,
                Poids = 0,
                Taille = 0,
                Provenance = "Chine",
                DateMiseEnLigne = new DateTime(2024, 05, 29),
                Fournisseur = "Fournisseur",
                MotsCles = "motscles1,motscles2",
            };

            mockArticleRepository.Setup(repo => repo.Update(entity)).Returns(false);

            IArticleBLLRepository repository = new ArticleBLLService(mockArticleRepository.Object);

            bool success = repository.Update(entity);

            Assert.False(success);
            mockArticleRepository.Verify(repo => repo.Update(entity), Times.Once);
        }

        #endregion
    }
}
