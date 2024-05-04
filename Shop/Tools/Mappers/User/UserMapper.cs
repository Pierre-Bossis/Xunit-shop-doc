using Shop.DAL.Entities;
using Shop.Models.User;

namespace Shop.Tools.Mappers.User
{
    public static class UserMapper
    {
        public static ConnectedUserDTO ToDto(this UserEntity e)
        {
            if (e is null) return null;

            ConnectedUserDTO dto = new()
            {
                Id = e.Id,
                Email = e.Email,
                Nom = e.Nom,
                Prenom = e.Prenom,
                DateCreation = e.DateCreation,
                IsAdmin = e.IsAdmin,
            };

            return dto;
        }
    }
}
