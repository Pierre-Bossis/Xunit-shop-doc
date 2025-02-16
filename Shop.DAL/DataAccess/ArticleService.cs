﻿using Dapper;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.DataAccess
{
    public class ArticleService : IArticleRepository
    {
        private readonly DbConnection _connection;

        public ArticleService(DbConnection connection)
        {
            _connection = connection;
        }
        public bool Create(ArticleEntity article)
        {
            string sql = @"
            INSERT INTO [Article] (
            Nom, Description, Categorie, Quantite, Image, Prix, Poids, Taille, Provenance,
            Fournisseur, MotsCles
            ) VALUES (
            @Nom, @Description, @Categorie, @Quantite, @Image, @Prix, @Poids, @Taille, @Provenance,
            @Fournisseur, @MotsCles);";

            try
            {
                int rowsAffected = _connection.Execute(sql, new
                {
                    article.Nom,
                    article.Description,
                    article.Categorie,
                    article.Quantite,
                    article.Image,
                    article.Prix,
                    article.Poids,
                    article.Taille,
                    article.Provenance,
                    article.Fournisseur,
                    article.MotsCles
                });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public string DeleteByReference(int reference)
        {
            string recupImage = "SELECT Image FROM [Article] WHERE Reference = @reference";
            string imageToDelete = _connection.QueryFirstOrDefault<string>(recupImage, new { reference = reference });

            string sql = "DELETE FROM [Article] WHERE Reference = @reference";
            var rowsAffected = _connection.Execute(sql, new { reference = reference });

            return imageToDelete;
        }

        public IEnumerable<ArticleEntity> GetAll()
        {
            string sql = "SELECT * FROM [Article]";
            IEnumerable<ArticleEntity> article = _connection.Query<ArticleEntity>(sql);
            return article;
        }

        public ArticleEntity GetByReference(int reference)
        {
            string sql = "SELECT * FROM [Article] WHERE Reference = @reference";
            ArticleEntity article = _connection.QueryFirstOrDefault<ArticleEntity>(sql, new { reference = reference });
            return article;
        }

        public IEnumerable<ArticleEntity> Search(string nom)
        {
            string sql = "SELECT * FROM [Article] WHERE Nom LIKE '%' + @nom + '%'";
            IEnumerable<ArticleEntity> articles = _connection.Query<ArticleEntity>(sql, new { nom });

            return articles;
        }

        public bool Update(ArticleEntity article)
        {
            string sql = "UPDATE [Article] SET Nom = @Nom, Description = @Description, Categorie = @Categorie, Quantite = @Quantite," +
                "Prix = @Prix, Poids = @Poids, Taille = @Taille, Provenance = @Provenance, Fournisseur = @Fournisseur, MotsCles = @MotsCles" +
                " WHERE Reference = @Reference;";
            int rowsAffected = _connection.Execute(sql,article);
            return rowsAffected > 0;
        }
    }
}
