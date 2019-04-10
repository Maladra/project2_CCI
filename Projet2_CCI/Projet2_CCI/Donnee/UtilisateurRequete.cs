using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    /// <summary>
    /// Représente un utilisateur retourné au moment de la connexion
    /// </summary>
    class UtilisateurConnexion
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }  
        public string Groupe { get; set; }
        public string Login { get; set; }
        public UtilisateurConnexion(string nom, string prenom, string groupe, string login)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Groupe = groupe;
            this.Login = login;
        }
    }
}

