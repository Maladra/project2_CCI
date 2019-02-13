using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class SnowboardRequete
    {
        public string Marque { get; set; } // marque du snow
        public string Genre { get; set; } // sexe homme femme enfant
        public string Niveau { get; set; } // debutant intermediaire expert
        public string Style { get; set; } // Freestyle Park, freeride, carving, polyvalent
        public decimal Prix { get; set; }
        public int Stock { get; set; }

            // CONSTRUCTEUR
        public SnowboardRequete(string marque, string genre, string niveau, string style, decimal prix, int stock)
        {
            this.Marque = marque;
            this.Genre = genre;
            this.Niveau = niveau;
            this.Style = style;
            this.Prix = prix;
            this.Stock = stock;
        }
    }
}
