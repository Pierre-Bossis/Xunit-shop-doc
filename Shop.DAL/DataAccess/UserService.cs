using Dapper;
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
    public class UserService : IUserRepository
    {
        private readonly DbConnection _connection;

        public UserService(DbConnection connection)
        {
            _connection = connection;
        }
        public string CheckPassword(string email)
        {
            string sql = "SELECT MotDePasse FROM [User] WHERE Email = @email";

            string? mdp = _connection.QueryFirstOrDefault<string>(sql, new { email = email });

            return mdp;
        }

        public UserEntity Login(string email)
        {
            string sql = "SELECT * FROM [User] WHERE Email = @email";
            return _connection.QueryFirst<UserEntity>(sql, new {email = email});
        }

        public UserEntity Register(string email, string nom, string prenom, string motDePasse)
        {
            //"OUTPUT INSERTED.Id permet de récupérer l'UNIQUEIDENTIFIER généré.
            string sql = "INSERT INTO [User](Email,Nom,Prenom,MotDePasse)" +
            "OUTPUT INSERTED.Id VALUES(@email,@nom,@prenom,@motDePasse);";

            Guid userId = _connection.QuerySingleOrDefault<Guid>(sql, new { email, nom, prenom, motDePasse });
            if(userId.ToString().Length > 0)
            {
                string sql2 = "SELECT * FROM [User] WHERE Id = @id";
                return _connection.QuerySingleOrDefault<UserEntity>(sql2, new { id = userId });
            }

            return null;
        }
    }
}
