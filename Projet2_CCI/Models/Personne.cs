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
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] PasswordByte { get; set; }
        public string Groupe { get; set; }

        public Employe(string nom, string prenom, string login, string password, string groupe) : base(nom, prenom)
        {
            this.Login = login;
            this.Password = password;
            this.Groupe = groupe;

        }
        public Employe(string nom, string prenom, string login, byte[] passwordByte, string groupe) : base(nom, prenom)
        {
            this.Login = login;
            this.PasswordByte = passwordByte;
            this.Groupe = groupe;
        }



        public override string ToString()
        {
            return $"{Nom} {Prenom}\nLogin: {Login}";
        }
    }

  
    public class Client : Personne
    {
        public string NumeroTelephone { get; set; }
        public Client (string nom, string prenom, string numeroTelephone) : base(nom, prenom) 
        {
            this.NumeroTelephone = numeroTelephone;
        }

    }
    public class ClientRequete : Client
    {
        public long IdClient { get; set; }
        public ClientRequete(long idClient,string nom, string prenom, string numeroTelephone) : base(nom,prenom,numeroTelephone)
        {
            this.IdClient = idClient;
        }
    }


}
