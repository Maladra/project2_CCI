using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    class Location // client, type de paiement(cb cheque espece), prix,
    {
        public string MoyenPaiement { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal PrixTotalEuroHt { get; set; }
        public decimal PrixTotalDollarHt { get; set; }
        public decimal TVA { get; set; }
        public int Quantite { get; set; }      

        // CONSTRUCTEUR
        public Location(string nom, string prenom, string moyenPaiement, int quantite, decimal prixTotalEuroHt, decimal prixTotalDollarHt, DateTime dateDebut, DateTime dateFin, decimal TVA)
        {
            this.MoyenPaiement = moyenPaiement;
            this.Nom = nom;
            this.Prenom = prenom;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.PrixTotalEuroHt = prixTotalEuroHt;
            this.PrixTotalDollarHt = prixTotalDollarHt;
            this.TVA = TVA;
            this.Quantite = quantite;

        }

    }
}
