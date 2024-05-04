namespace Shop.Models.User
{
    public class ConnectedUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateCreation { get; set; }
        public bool IsAdmin { get; set; }
    }
}
