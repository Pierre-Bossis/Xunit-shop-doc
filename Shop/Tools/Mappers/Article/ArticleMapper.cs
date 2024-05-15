using Shop.DAL.Entities;
using Shop.Models.Article;

namespace Shop.Tools.Mappers.Article
{
    public static class ArticleMapper
    {
        #region CREATE

        public static ArticleEntity ToEntity(this ArticleCreateDTO dto, string relativePath)
        {
            if (dto is null) return null;

            ArticleEntity e = new()
            {
                Nom = dto.Nom,
                Description = dto.Description,
                Categorie = dto.Categorie,
                Quantite = dto.Quantite,
                Image = relativePath,
                Prix = dto.Prix,
                Poids = dto.Poids,
                Taille = dto.Taille,
                Provenance = dto.Provenance,
                Fournisseur = dto.Fournisseur,
                MotsCles = dto.MotsCles,
            };

            return e;
        }

        #endregion

        #region GET

        public static ArticleDTO ToDtoGET(this ArticleEntity entity)
        {
            if (entity is null) return null;

            string base64String = string.Empty;

            if (!string.IsNullOrEmpty(entity.Image) && File.Exists(entity.Image))
            {
                byte[] imageBytes = File.ReadAllBytes(entity.Image);
                base64String = Convert.ToBase64String(imageBytes);
            }

            ArticleDTO a = new()
            {
                Reference = entity.Reference,
                Nom = entity.Nom,
                Description = entity.Description,
                Categorie = entity.Categorie,
                Quantite = entity.Quantite,
                Image = base64String,
                Prix = entity.Prix,
                QuantiteVendue = entity.QuantiteVendue,
                NombreRecommandations = entity.NombreRecommandations,
                Poids = entity.Poids,
                Taille = entity.Taille,
                Provenance = entity.Provenance,
                DateMiseEnLigne = entity.DateMiseEnLigne,
                Fournisseur = entity.Fournisseur,
                MotsCles = entity.MotsCles,
            };

            return a;
        }

        #endregion

        #region Update
        public static ArticleEntity ToEntity(this ArticleUpdateDTO articleUpdateDTO)
        {
            if (articleUpdateDTO is null) return null;

            ArticleEntity e = new ArticleEntity()
            {
                Reference = articleUpdateDTO.Reference,
                Nom = articleUpdateDTO.Nom,
                Description = articleUpdateDTO.Description,
                Categorie = articleUpdateDTO.Categorie,
                Quantite = articleUpdateDTO.Quantite,
                //Image = articleUpdateDTO.Image,
                Prix = articleUpdateDTO.Prix,
                Poids = articleUpdateDTO.Poids,
                Taille = articleUpdateDTO.Taille,
                Provenance = articleUpdateDTO.Provenance,
                Fournisseur = articleUpdateDTO.Fournisseur,
                MotsCles = articleUpdateDTO.MotsCles,
            };

            return e;
        }

        //public static ArticleUpdateDTO ToDtoUpdate(this ArticleEntity entity)
        //{
        //    if (entity is null) return null;

        //    ArticleUpdateDTO a = new()
        //    {
        //        Nom = entity.Nom,
        //        Description = entity.Description,
        //        Categorie = entity.Categorie,
        //        Quantite = entity.Quantite,
        //        Image = entity.Image,
        //        Prix = entity.Prix,
        //        Poids = entity.Poids,
        //        Taille = entity.Taille,
        //        Provenance = entity.Provenance,
        //        Fournisseur = entity.Fournisseur,
        //        MotsCles = entity.MotsCles,
        //    };

        //    return a;
        //}

        #endregion

    }
}
