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
        public string TypePaiement { get; set; }
        public int Quantite { get; set; }
        public decimal Prix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        // CONSTRUCTEUR
        public Location(string nom, string prenom, string typePaiement, int quantite, decimal prix, DateTime dateDebut, DateTime dateFin)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.TypePaiement = typePaiement;
            this.Quantite = quantite;
            this.Prix = prix;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
        }

    }
}
