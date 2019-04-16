using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    public class Location // client, type de paiement(cb cheque espece), prix,
    {
        public string MoyenPaiement { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public List<SnowboardRequeteId> ListeSnowboard { get; set; }
        public decimal Tva { get; set; }
        public string Etat { get; set; }

        public Location(string nomClient, string prenomClient, string moyenPaiement,
            DateTime dateDebut, DateTime dateFin, List<SnowboardRequeteId> listeSnowboard ,decimal tva, string etat)
        {
            this.MoyenPaiement = moyenPaiement;
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.ListeSnowboard = listeSnowboard;
            this.Tva = tva;
            this.Etat = etat;
        }

    }
}
