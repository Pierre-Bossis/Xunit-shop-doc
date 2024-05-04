using Shop.BLL.Interfaces;
using Shop.DAL.Entities;
using Shop.DAL.Repositories;
using Crypt = BCrypt.Net;

namespace Shop.BLL.Services
{
    public class UserBLLService : IUserBLLRepository
    {
        private readonly IUserRepository _repo;

        public UserBLLService(IUserRepository repo)
        {
            _repo = repo;
        }
        public UserEntity Login(string email, string motDePasse)
        {
            string PwdCheck = _repo.CheckPassword(email);

            //si l'email n'existe pas le PwdCheck sera null, et ca fera crasher la méthode Verify
            if (PwdCheck is null) return null;

            //si ca match
            else if(Crypt.BCrypt.Verify(motDePasse,PwdCheck))
            {
                return _repo.Login(email);
            }

            //si ca match pas
            return null;
        }

        public UserEntity Register(string email, string nom, string prenom, string motDePasse)
        {
            string hash = Crypt.BCrypt.HashPassword(motDePasse);

            return _repo.Register(email,nom,prenom,hash);
        }
    }
}
