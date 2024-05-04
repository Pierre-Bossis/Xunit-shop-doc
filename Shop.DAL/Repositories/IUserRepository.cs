using Shop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
    public interface IUserRepository
    {
        string CheckPassword(string email);
        UserEntity Login(string email);
        UserEntity Register(string email, string nom, string prenom, string motDePasse);
    }
}
