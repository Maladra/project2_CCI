using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class UtilisateurConnexion
    {
    public string Nom { get; set; }
    public string Prenom { get; set; }  
    public string Groupe { get; set; }
        public UtilisateurConnexion(string nom, string prenom, string groupe)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Groupe = groupe;
        }
    }
}

