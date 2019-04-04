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
    class SnowboardAddRequete
    {
        public string Nom { get; set;}
        public int Marque { get; set; }
        public int Genre { get; set; }
        public int Niveau { get; set; }
        public int Style { get; set; }
        public int PrixEuro { get; set; }
        public int PrixDollar { get; set; }
        public int Stock { get; set; }

            // CONSTRUCTEUR
            public SnowboardAddRequete(string nom, int marque, int genre, int niveau, int style, int prixEuro, int prixDollar, int stock)
            {
            this.Nom = nom;
            this.Marque = marque;
            this.Genre = genre;
            this.Niveau = niveau;
            this.Style = style;
            this.PrixEuro = prixEuro;
            this.PrixDollar = prixDollar;
            this.Stock = stock;
            }   
    }
}




// TODO: MODIFICATION BASE DE DONNÉE (SUPPRIMER TABLE Niveau_snowboard, Genre_Snowboard)
// TODO: MODIFICATION table Planche_snowboard (les FK si modification de la DB) 
