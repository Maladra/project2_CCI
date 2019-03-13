using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    class Snowboard
    {
        public string Nom { get; set; } // nom du modele
        public string Marque { get; set; } // marque du snow
        public string Genre { get; set; } // sexe homme femme enfant
        public string Niveau { get; set; } // debutant intermediaire expert
        public string Style { get; set; } // Freestyle Park, freeride, carving, polyvalent
        public decimal Prix_euro { get; set; }
        public decimal Prix_dollar { get; set; }

        // CONSTRUCTEUR
        public Snowboard(string nom, string marque, string genre, string niveau, string style, decimal prix_euro, decimal prix_dollar)
        {
            this.Nom = nom;
            this.Marque = marque;
            this.Genre = genre;
            this.Niveau = niveau;
            this.Style = style;
            this.Prix_euro = prix_euro;
            this.Prix_dollar = prix_dollar;
        }
    }

}
