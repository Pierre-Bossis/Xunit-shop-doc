using System.ComponentModel.DataAnnotations;

namespace Shop.Models.User
{
    public class RegisterFormDTO
    {
        [EmailAddress,Required,MinLength(2),MaxLength(100)]
        public string Email { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Nom { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Prenom { get; set; }
        [DataType(DataType.Password),Required,MinLength(2),MaxLength(50)]
        public string MotDePasse { get; set; }
        [DataType(DataType.Password), Required, MinLength(2), MaxLength(50)]
        [Compare("MotDePasse", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string ConfirmMotDePasse { get; set; }
    }
}
