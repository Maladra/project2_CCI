using Models;
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
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal Tva { get; set; }
        public string Etat { get; set; }
        public Location(string moyenPaiement, DateTime dateDebut, DateTime dateFin, decimal tva, string etat)
        {
            this.MoyenPaiement = moyenPaiement;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.Tva = tva;
            this.Etat = etat;
        }
        public override string ToString()
        {
            return "Du : " + this.DateDebut.ToShortDateString() + "\n" + "Jusqu'au : " + this.DateFin.ToShortDateString();
        }
    }

    public class LocationAvecClient
    {
        public string MoyenPaiement { get; set; }
        public Client ClientLocation { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal Tva { get; set; }
        public string Etat { get; set; }
        public LocationAvecClient(Client clientLocation, string moyenPaiement,
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

    public class LocationAvecListeSnowboard : LocationAvecClient
    {
        public List<SnowboardRequeteId> ListeSnowboard { get; set; }

        public LocationAvecListeSnowboard(Client clientLocation, string moyenPaiement, DateTime dateDebut, DateTime dateFin,
             decimal tva, string etat, List<SnowboardRequeteId> listeSnowboard)
            : base(clientLocation, moyenPaiement, dateDebut, dateFin, tva, etat)
        {
            this.ListeSnowboard = listeSnowboard;
        }

    }

    public class LocationAvecId : LocationAvecClient
    {
        public long IdLocation { get; set; }

        public LocationAvecId(Client clientLocation, string moyenPaiement,
            DateTime dateDebut, DateTime dateFin, decimal tva, string etat, long idLocation)
            : base(clientLocation, moyenPaiement, dateDebut, dateFin, tva, etat)
        {
            this.IdLocation = idLocation;
        }

        public class DynamicLocationId  : ViewModelBase
        {
            public long IdLocation { get; set; }
            public string MoyenPaiement { get; set; }
            public DateTime DateDebut { get; set; }
            public DateTime DateFin { get; set; }
            public decimal Tva { get; set; }
            public string Etat { get; set; }
            public DynamicLocationId(long idLocation,string moyenPaiement, DateTime dateDebut, DateTime dateFin, decimal tva, string etat)
            {
                this.IdLocation = idLocation;
                this.MoyenPaiement = moyenPaiement;
                this.DateDebut = dateDebut;
                this.DateFin = dateFin;
                this.Tva = tva;
                this.Etat = etat;
            }
            public override string ToString()
            {
                return "Du : " + this.DateDebut.ToShortDateString() + "\n" + "Jusqu'au : " + this.DateFin.ToShortDateString();
            }

            public string EtatLocation
            {
                get { return this.Etat; }
                set { this.Etat = value; this.OnPropertyChange(); }
            }
        }
    }
}
