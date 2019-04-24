using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.Donnee
{

    /// <summary>
    /// Class retournée par requete SQL Snowboard <see cref="SQLHelper.SQLitePlancheRead"/>
    /// </summary>
    public class SnowboardRequete : Snowboard
    {

        public int Stock { get; set; }
        public SnowboardRequete(string nom, Marque marque, Genre genre, Niveau niveau, Style style,
            decimal prixEuro, decimal prixDollar, int stock) :
            base(nom, marque, genre, niveau, style, prixEuro, prixDollar)
        {
            this.Stock = stock;
        }
    }

    public class SnowboardRequeteId : SnowboardRequete
    {
        public long IdSnowboard { get; set; }

        public SnowboardRequeteId(long idSnowboard, string nom, Marque marque, Genre genre,
            Niveau niveau, Style style, decimal prixEuro, decimal prixDollar, int stock) :
            base(nom, marque, genre, niveau, style, prixEuro, prixDollar, stock)
        {
            this.IdSnowboard = idSnowboard;
        }

    }
    public class PlancheLouee
    {
        public long IdPlanche { get; set; }
        public int Quantite { get; set; }

        public PlancheLouee(long idPlanche, int quantite)
        {
            this.IdPlanche = idPlanche;
            this.Quantite = quantite;
        }

    }
}
