using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Article
{
    public class ArticleUpdateDTO
    {
        public int Reference { get; set; }
        [MinLength(2), MaxLength(255), Required]
        public string Nom { get; set; }
        [MinLength(2), MaxLength(1000), Required]
        public string Description { get; set; }
        [MinLength(2), MaxLength(255), Required]
        public string Categorie { get; set; }
        [Range(0, 1000), Required]
        public int Quantite { get; set; }
        //[MinLength(2), MaxLength(1000), Required]
        //public string Image { get; set; }
        [Range(1, 10000), Required]
        public decimal Prix { get; set; }
        [Required]
        public decimal Poids { get; set; }
        [Required]
        public decimal? Taille { get; set; }
        [MinLength(2), MaxLength(100), Required]
        public string Provenance { get; set; }
        [MinLength(2), MaxLength(100), Required]
        public string Fournisseur { get; set; }
        [MinLength(2), MaxLength(1000), Required]
        public string MotsCles { get; set; }
    }
}
