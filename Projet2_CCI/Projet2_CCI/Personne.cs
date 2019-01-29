using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    public abstract class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        // CONSTRUCTEUR
        public Personne(string nom, string prenom)
        {
            this.Nom = nom;
            this.Prenom = prenom;
        }

    }

    public class Employe : Personne
    {
        public string groupe { get; set; }
        public Employe(string nom, string prenom, string groupe) : base(nom, prenom)
        {
        }
    }
  
    class Client : Personne
    {
        public string NumeroTelephone { get; set; }
        public bool LocationEnCours { get; set; }
        public int NbLocation { get; set; }
        public Client (string nom, string prenom, bool locationEnCours, int nbLocation, string numeroTelephone) : base(nom, prenom) 
        {
            this.LocationEnCours = locationEnCours;
            this.NbLocation = NbLocation;
            this.NumeroTelephone = numeroTelephone;
        }

    }
}
