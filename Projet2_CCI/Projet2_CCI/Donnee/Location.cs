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
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public Snowboard ListeSnowboard { get; set; }
        public decimal PrixTotalEuroHt { get; set; }
        public decimal PrixTotalDollarHt { get; set; }
        public decimal TVA { get; set; }
        public decimal PrixTotalEuro { get; set; }
        public decimal PrixTotalDollar { get; set; }

        public Location(string nomClient, string prenomClient, string moyenPaiement,
            decimal prixTotalEuroHt, decimal prixTotalDollarHt,
            DateTime dateDebut, DateTime dateFin, Snowboard listeSnowboard ,
            decimal TVA, decimal prixTotalEuro, decimal prixTotalDollar)
        {
            this.MoyenPaiement = moyenPaiement;
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.PrixTotalEuroHt = prixTotalEuroHt;
            this.PrixTotalDollarHt = prixTotalDollarHt;
            this.ListeSnowboard = listeSnowboard;
            this.TVA = TVA;
            this.PrixTotalDollar = prixTotalDollar;
            this.PrixTotalEuro = prixTotalEuro;


        }

    }
}
