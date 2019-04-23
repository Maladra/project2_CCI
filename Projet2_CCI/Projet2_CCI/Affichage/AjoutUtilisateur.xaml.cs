using Projet2_CCI.DAL;
using Projet2_CCI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Projet2_CCI
{
    /// <summary>
    /// Logique d'interaction pour AjoutUtilisateur.xaml
    /// </summary>
    public partial class AjoutUtilisateur : Window
    {
        /// <summary>
        /// Ajoute un Utilisateur dans la base de donnee verifie les champs et 
        /// insert dans la BD si tous les champs sont valides
        /// </summary>
        private void addUser()
        {

            string nomEmploye = nom.Text;
            string prenomEmploye = prenom.Text;
            string loginEmploye = login.Text;
            string passwordEmploye = password.Password;
            string groupeEmploye = listeGroupe.Text;
            if (UtilsClass.VerifString(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye))
            {

                Employe employe = new Employe(nom.Text, prenom.Text, login.Text, password.Password, listeGroupe.Text);
                if (RequeteSqlUser.SQLiteAddUser(employe))
                {
                    MessageBox.Show("L'utilisateur a été crée");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur pendant la requête");
                }

            }
            else
            {
                MessageBox.Show("Merci de renseigner tous les champs");
            }
        }

        public AjoutUtilisateur()
        {
            InitializeComponent();
            this.listeGroupe.Items.Add("Vendeur");
            this.listeGroupe.Items.Add("Administrateur");
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            addUser();
        }

    }
}
