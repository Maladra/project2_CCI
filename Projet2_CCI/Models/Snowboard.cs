using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    public class Snowboard
    {
        public string Nom { get; set; }
        public Marque Marque { get; set; }
        public Genre Genre { get; set; }
        public Niveau Niveau { get; set; }
        public Style Style { get; set; }
        public decimal PrixEuro { get; set; }
        public decimal PrixDollar { get; set; }

        public Snowboard(string nom, Marque marque, Genre genre, Niveau niveau, Style style, decimal prixEuro, decimal prixDollar)
        {
            this.Nom = nom;
            this.Marque = marque;
            this.Genre = genre;
            this.Niveau = niveau;
            this.Style = style;
            this.PrixEuro = prixEuro;
            this.PrixDollar = prixDollar;
        }


    }






}
