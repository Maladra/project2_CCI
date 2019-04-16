using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{

    public class Location
    {
        public string MoyenPaiement { get; set; }
        public Client ClientLocation { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal Tva { get; set; }
        public string Etat { get; set; }
        public Location(Client clientLocation, string moyenPaiement,
            DateTime dateDebut, DateTime dateFin, decimal tva, string etat)
        {
            this.MoyenPaiement = moyenPaiement;
            this.ClientLocation = clientLocation;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.Tva = tva;
            this.Etat = etat;
        }
    }

    public class LocationAvecListeSnowboard : Location 
    {
        public List<SnowboardRequeteId> ListeSnowboard { get; set; }

        public LocationAvecListeSnowboard(Client clientLocation, string moyenPaiement, DateTime dateDebut, DateTime dateFin,
             decimal tva, string etat, List<SnowboardRequeteId> listeSnowboard)
            : base (clientLocation, moyenPaiement, dateDebut, dateFin, tva,etat)
        {
            this.ListeSnowboard = listeSnowboard;
        }
    }
}
