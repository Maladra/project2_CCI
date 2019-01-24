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
        public Employe(string nom, string prenom) : base(nom, prenom)
        {


        }
    }
  
    class Client : Personne // location en cours
    {
        public Client (string nom, string prenom) : base(nom, prenom) 
        {

        }
    }
}
