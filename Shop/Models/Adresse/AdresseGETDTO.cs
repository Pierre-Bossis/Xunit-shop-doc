﻿namespace Shop.Models.Adresse
{
    public class AdresseGETDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public int CodePostal { get; set; }
        public string Localite { get; set; }
        public string Pays { get; set; }
    }
}
