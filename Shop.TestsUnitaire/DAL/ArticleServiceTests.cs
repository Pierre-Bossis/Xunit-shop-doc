using Dapper;
using Microsoft.Data.Sqlite;
using Moq;
using Shop.DAL.DataAccess;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.TestsUnitaire.DAL
{
    public class ArticleServiceTests : IDisposable
    {
        private readonly SqliteConnection _sqliteConnection;
        private readonly ArticleService _articleService;
        public ArticleServiceTests()
        {
            _sqliteConnection = new SqliteConnection("DataSource=:memory:");
            _sqliteConnection.Open();
            _sqliteConnection.Execute(
            @"CREATE TABLE Article
            (
                Reference INTEGER PRIMARY KEY AUTOINCREMENT,
                Nom TEXT NOT NULL,
                Description TEXT NOT NULL,
                Categorie TEXT NOT NULL,
                Quantite INTEGER DEFAULT 0,
                Image TEXT NOT NULL,
                Prix DECIMAL(18,2) NOT NULL,
                QuantiteVendue INTEGER NOT NULL DEFAULT 0,
                NombreRecommandations INTEGER NOT NULL DEFAULT 0,
                Poids DECIMAL(18,2) NOT NULL,
                Taille DECIMAL(18,2) NOT NULL DEFAULT 0,
                Provenance TEXT NOT NULL,
                DateMiseEnLigne DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                Fournisseur TEXT NOT NULL,
                MotsCles TEXT NOT NULL
            )"
                );

            _sqliteConnection.Execute(
                @"INSERT INTO [Article](Nom,Description,Categorie,Quantite,Image,Prix,Poids,Taille,Provenance,Fournisseur,MotsCles)
                VALUES ('Ordinateur Portable HP Pavilion',
                    'L''ordinateur portable HP Pavilion est équipé d''un processeur Intel Core i5, de 8 Go de RAM et d''un disque dur SSD de 512 Go. Il dispose d''un écran Full HD de 15,6 pouces et est idéal pour le travail et le divertissement.',
                    'Informatique',
                    15,
                    'https://example.com/images/hp_pavilion.jpg',
                    799.99,
                    1.8,
                    15.6,
                    'États-Unis',
                    'HP Inc.',
                    'Ordinateur portable, HP, Intel Core i5, SSD, Full HD, Windows 10'
                )"
                );

            _sqliteConnection.Execute(
                @"INSERT INTO [Article](Nom,Description,Categorie,Quantite,Image,Prix,Poids,Taille,Provenance,Fournisseur,MotsCles)
            VALUES
            ('Souris Gaming Logitech G Pro',
                'La souris gaming Logitech G Pro est conçue pour offrir une précision et une réactivité exceptionnelles. Elle dispose de boutons programmables et d''un capteur optique avancé.',
                'Informatique',
                50,
                'https://example.com/images/logitech_gpro_mouse.jpg',
                69.99,
                0.12,
                0.20,
                'Suisse',
                'Logitech',
                'Souris gaming, Logitech, Capteur optique, Boutons programmables'
            );"
            );

            _sqliteConnection.Execute(
               @"INSERT INTO [Article](Nom,Description,Categorie,Quantite,Image,Prix,Poids,Taille,Provenance,Fournisseur,MotsCles)
            VALUES
            (
                'Casque Audio Sony WH-1000XM4',
                'Le casque audio Sony WH-1000XM4 offre un son haute résolution et une réduction de bruit active exceptionnelle. Il est confortable et idéal pour les longues sessions d''écoute.',
                'Audio',
                40,
                'https://example.com/images/sony_wh1000xm4_headphones.jpg',
                349.99,
                0.25,
                0.54,
                'Japon',
                'Sony Corporation',
                'Casque audio, Sony, Réduction de bruit, Bluetooth, Hi-Res Audio'
            );"
            );
            _articleService = new ArticleService( _sqliteConnection );
        }


        #region GetAll Tests

        [Fact]
        public void GetAll_Should_Return_Data()
        {
            //arrange

            //act
            IEnumerable<ArticleEntity> articles = _articleService.GetAll();

            //assert
            Assert.NotNull(articles);
            Assert.Equal(3, articles.Count());
            ///Assert.Single(articles);
        }

        #endregion

        #region Delete Tests

        [Fact]
        public void Delete_Should_Return_String()
        {
            //arrange

            //act
            string pathImageToDelete = _articleService.DeleteByReference(1);

            //assert
            Assert.NotNull(pathImageToDelete);
        }

        [Fact]
        public void Delete_Should_Return_Null()
        {
            //arrange

            //act
            string pathImageToDelete = _articleService.DeleteByReference(1000);

            //assert
            Assert.Null(pathImageToDelete);
        }

        #endregion

        #region GetByReference Tests

        [Fact]
        public void GetByReference_Should_Return_One_Entity()
        {
            //arrange

            //act
            ArticleEntity entity = _articleService.GetByReference(1);

            //assert
            Assert.NotNull(entity);
            Assert.Equal(1, entity.Reference);
        }

        [Fact]
        public void GetByReference_Should_Return_Null()
        {
            //arrange

            //act
            ArticleEntity entity = _articleService.GetByReference(1000);

            //assert
            Assert.Null(entity);
        }

        #endregion

        #region Update Tests

        [Fact]
        public void Update_Should_Return_True()
        {
            //arrange
            ArticleEntity entity = new ArticleEntity()
            {
                Reference = 1,
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

            //act
            bool success = _articleService.Update(entity);
            ArticleEntity entityModified = _articleService.GetByReference(1);

            //assert
            Assert.True(success);
            Assert.Equal("nom", entity.Nom);
        }

        [Fact]
        public void Update_Should_Return_False()
        {
            //arrange
            ArticleEntity entity = new ArticleEntity()
            {
                Reference = 500,
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

            //act
            bool success = _articleService.Update(entity);

            //assert
            Assert.False(success);
        }

        #endregion

        #region Search Tests

        //requete sql qui fonctionne pour sql server mais pas pour sqlite
        //[Fact]
        //public void Search_Sould_Return_Entities()
        //{
        //    //arrange

        //    //act
        //    IEnumerable<ArticleEntity> articles = _articleService.Search("on");

        //    //assert
        //    Assert.NotNull(articles);
        //    Assert.Equal(2, articles.Count());
        //}

        //[Fact]
        //public void Search_Sould_Return_Null()
        //{
        //    //arrange

        //    //act
        //    IEnumerable<ArticleEntity> articles = _articleService.Search("rgedf");

        //    //assert
        //    Assert.Empty(articles);
        //}

        #endregion

        #region Create Tests

        [Fact]
        public void Create_Should_Return_True()
        {
            //arrange
            ArticleEntity entity = new ArticleEntity()
            {
                Nom = "nom",
                Description = "description",
                Categorie = "categorie",
                Quantite = 3,
                Image = "path",
                Prix = 50.25m,
                QuantiteVendue = 0,
                NombreRecommandations = 0,
                Poids = 0.20m,
                Taille = 0.54m,
                Provenance = "Chine",
                Fournisseur = "Fournisseur",
                MotsCles = "motscles1,motscles2",
            };

            //act
            int numberEntriesBefore = _articleService.GetAll().Count();
            bool success = _articleService.Create(entity);
            int numberEntriesAfter = _articleService.GetAll().Count();

            //assert
            Assert.True(success);
            Assert.NotEqual(numberEntriesBefore, numberEntriesAfter);
        }

        [Fact]
        public void Create_Should_Return_False()
        {
            //arrange
            ArticleEntity entity = new ArticleEntity()
            {
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
                Fournisseur = "Fournisseur",
                MotsCles = "motscles1,motscles2",
            };

            //act
            int numberEntriesBefore = _articleService.GetAll().Count();
            bool success = _articleService.Create(entity);
            int numberEntriesAfter = _articleService.GetAll().Count();

            //assert
            Assert.False(success);
            Assert.Equal(numberEntriesBefore, numberEntriesAfter);
        }

        #endregion

        public void Dispose()
        {
            _sqliteConnection.Close();
            _sqliteConnection.Dispose();
        }
    }
}
