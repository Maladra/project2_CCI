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

            // CONSTRUCTEUR
            public SnowboardRequete(string nom, string marque, string genre, string niveau, string style, int prixEuro, int prixDollar, int stock) : base(nom, marque, genre, niveau, style, prixEuro, prixDollar)
            {
                this.Stock = stock;
            }

        public SnowboardRequete Clone()
        => new SnowboardRequete(this.Nom, this.Marque, this.Genre, this.Niveau, this.Style, this.PrixEuro, this.PrixDollar, this.Stock);
    }
}




// TODO: MODIFICATION BASE DE DONNÉE (SUPPRIMER TABLE Niveau_snowboard, Genre_Snowboard)
// TODO: MODIFICATION table Planche_snowboard (les FK si modification de la DB) 
