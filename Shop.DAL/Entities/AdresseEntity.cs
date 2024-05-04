using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Entities
{
    public class AdresseEntity
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public int CodePostal { get; set; }
        public string Localite { get; set; }
        public string Pays { get; set; }
    }
}
