using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.BLL.Interfaces;
using Shop.Controllers;
using Shop.DAL.Entities;
using Shop.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.TestsUnitaire.Controllers
{
    public class ArticleControllerTests
    {
        private Mock<IArticleBLLRepository> mockArticleBLLRepository = new Mock<IArticleBLLRepository>();
        private Mock<IWebHostEnvironment> mockHostingEnvironment = new Mock<IWebHostEnvironment>();

        #region GetAll Tests
        [Fact]
        public void GetAll_Return_Empty()
        {
            //arrange
            List<ArticleEntity> list = new List<ArticleEntity>();

            mockArticleBLLRepository.Setup(repo => repo.GetAll()).Returns(list);

            ArticleController controller = new(mockArticleBLLRepository.Object, mockHostingEnvironment.Object);

            //act
            var result = controller.GetAll() as OkObjectResult;

            //assert
            Assert.NotNull(result);  // Vérifie que le résultat n'est pas null
            Assert.Equal(200, result.StatusCode);  // Vérifie que le statut est 200 (OK)
            Assert.Equal(0, (result.Value as IEnumerable<ArticleDTO>).Count());

            mockArticleBLLRepository.Verify(repo => repo.GetAll());
        }

        [Fact]
        public void GetAll_Return_Some_Elements()
        {
            //arrange
            List<ArticleEntity> list = new List<ArticleEntity>() { new(), new(), new() };

            mockArticleBLLRepository.Setup(repo => repo.GetAll()).Returns(list);

            ArticleController controller = new(mockArticleBLLRepository.Object, mockHostingEnvironment.Object);

            //act
            var result = controller.GetAll() as OkObjectResult;

            //assert
            Assert.NotNull(result);  // Vérifie que le résultat n'est pas null
            Assert.Equal(200, result.StatusCode);  // Vérifie que le statut est 200 (OK)
            Assert.IsAssignableFrom<IEnumerable<ArticleDTO>>(result.Value); // Vérifie que la valeur est bien une liste d'ArticleDTO
            Assert.Equal(3, (result.Value as IEnumerable<ArticleDTO>).Count());

            mockArticleBLLRepository.Verify(repo => repo.GetAll());
        }

        #endregion

        #region GetByReference Tests
        [Fact]
        public void GetByReference_Should_Return_OkResult()
        {
            //arrange
            int reference = 5;

            mockArticleBLLRepository.Setup(repo => repo.GetByReference(reference)).Returns(new ArticleEntity());
            ArticleController controller = new(mockArticleBLLRepository.Object, mockHostingEnvironment.Object);

            //act
            var result = controller.GetByReference(reference) as OkObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode );
            Assert.Equal(typeof(ArticleDTO), result.Value.GetType());
            Assert.IsAssignableFrom<ArticleDTO>(result.Value);
            mockArticleBLLRepository.Verify(repo => repo.GetByReference(reference));
        }

        [Fact]
        public void GetByReference_Should_Return_NotFoundResult()
        {
            //arrange
            int reference = 151651;

            mockArticleBLLRepository.Setup(repo => repo.GetByReference(reference)).Returns((ArticleEntity)null);
            ArticleController controller = new(mockArticleBLLRepository.Object, mockHostingEnvironment.Object);

            //act
            var result = controller.GetByReference(reference) as NotFoundObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Aucun article trouvé.", result.Value);
            mockArticleBLLRepository.Verify(repo => repo.GetByReference(reference));
        }

        #endregion

        #region DeleteByReference Tests
        [Fact]
        public void DeleteByReference_Should_Return_OkResult()
        {
            //arrange
            int reference = 5;

            mockArticleBLLRepository.Setup(repo => repo.DeleteByReference(reference)).Returns("PathImageToDelete");
            ArticleController controller = new(mockArticleBLLRepository.Object, mockHostingEnvironment.Object);

            //act
            var result = controller.DeleteByReference(reference) as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Contains("supprimé avec succès",result.Value.ToString());
            mockArticleBLLRepository.Verify( repo => repo.DeleteByReference(reference));
        }

        [Fact]
        public void DeleteByReference_Should_Return_NotFoundResult()
        {
            //arrange
            int reference = 5646451;

            mockArticleBLLRepository.Setup(repo => repo.DeleteByReference(reference)).Returns((string)null);
            ArticleController controller = new(mockArticleBLLRepository.Object,mockHostingEnvironment.Object);

            //act
            var result = controller.DeleteByReference(reference) as NotFoundObjectResult;

            //assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Contains("non trouvé", result.Value.ToString());
            mockArticleBLLRepository.Verify(repo => repo.DeleteByReference(reference));
        }

        #endregion
    }
}
