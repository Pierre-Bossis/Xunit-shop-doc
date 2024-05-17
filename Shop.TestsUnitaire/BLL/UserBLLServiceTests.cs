using BCrypt.Net;
using Moq;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;

namespace Shop.TestsUnitaire.BLL
{
    public class UserBLLServiceTests
    {
        #region Login Tests
        [Fact]
        public void Login_Should_Return_Entity_When_Valid_Credentials()
        {
            //Arrange - préparation des data

            // Crée un mock de IUserRepository pour simuler la couche de données
            var mockUserRepository = new Mock<IUserRepository>();

            // Configure le mock pour simuler une connexion réussie avec des identifiants valides
            mockUserRepository.Setup(repo => repo.CheckPassword("validEmail")).Returns(BCrypt.Net.BCrypt.HashPassword("validPassword"));
            mockUserRepository.Setup(repo => repo.Login("validEmail")).Returns(new UserEntity());

            // Crée une instance de UserBLLService avec le mock de UserRepository
            IUserBLLRepository repository = new UserBLLService(mockUserRepository.Object);

            // Act - lance l'action

            // Appelle la méthode Login avec des identifiants valides
            UserEntity result = repository.Login("validEmail", "validPassword");

            // Assert - test le résultat

            // Vérifie que l'objet retourné n'est pas null et est du bon type
            Assert.NotNull(result);
            Assert.IsType<UserEntity>(result);

        }

        [Fact]
        public void Login_Should_Return_Null_When_Invalid_Credentials()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repo => repo.CheckPassword("validEmail")).Returns(BCrypt.Net.BCrypt.HashPassword("validPassword"));
            mockUserRepository.Setup(repo => repo.Login("validEmail")).Returns(new UserEntity());

            IUserBLLRepository repository = new UserBLLService(mockUserRepository.Object);

            //act
            UserEntity result = repository.Login("invalidEmail", "invalidPassword");

            //assert
            Assert.Null(result);
        }
        #endregion

        #region Register Tests

        //[Fact]
        //public void Register_Should_Return_Entity_When_Valid()
        //{
        //    // Arrange
        //    var mockUserRepository = new Mock<IUserRepository>();

        //    mockUserRepository.Setup(repo => repo.Register("test@gmail.com", "testNom", "testPrenom", It.IsAny<string>()))
        //        .Returns(new UserEntity {DateCreation = DateTime.Now,Id = Guid.NewGuid(), IsAdmin = false, Email = "test@gmail.com", Nom = "testNom", Prenom = "testPrenom", MotDePasse = BCrypt.Net.BCrypt.HashPassword("test1234") });

        //    IUserBLLRepository repository = new UserBLLService(mockUserRepository.Object);

        //    // Act
        //    UserEntity result = repository.Register("test@gmail.com", "testNom", "testPrenom", "test1234");

        //    // Assert
        //    Assert.NotNull(result);
        //}

        #endregion
    }
}
