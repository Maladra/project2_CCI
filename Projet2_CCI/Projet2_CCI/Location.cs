using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    class Location // client, type de paiement(cb cheque espece), prix,
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string MoyenPaiement { get; set; }
        public int Quantite { get; set; }
        public decimal PrixTotalHT { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal TVA { get; set; }
        
        // CONSTRUCTEUR
        public Location(string nom, string prenom, string moyenPaiement, int quantite, decimal prixTotalHT, DateTime dateDebut, DateTime dateFin, decimal TVA)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.MoyenPaiement = moyenPaiement;
            this.Quantite = quantite;
            this.PrixTotalHT = prixTotalHT;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.TVA = TVA;
        }

    }
}
