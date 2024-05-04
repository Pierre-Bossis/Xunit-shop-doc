using Shop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IUserBLLRepository
    {
        UserEntity Login(string email, string motDePasse);
        UserEntity Register(string email, string nom, string prenom, string motDePasse);
    }
}
