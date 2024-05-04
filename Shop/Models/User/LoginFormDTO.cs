using System.ComponentModel.DataAnnotations;

namespace Shop.Models.User
{
    public class LoginFormDTO
    {
        [EmailAddress,Required]
        public string Email { get; set; }
        [DataType(DataType.Password),Required]
        public string MotDePasse { get; set; }
    }
}
