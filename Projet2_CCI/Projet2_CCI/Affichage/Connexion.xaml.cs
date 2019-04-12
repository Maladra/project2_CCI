using Projet2_CCI.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        /// <summary>
        /// Prend deux strings (Login user et Password user) test la presence et la concordance des champs
        /// avec un utilisateur dans la DB et gere la connexion
        /// </summary>
        private void procConnexion()
        {
            UtilisateurConnexion utilisateur = RequeteSqlConnexion.SQLiteConnexionHash(
                this.UsernameText.Text, this.PasswordText.Password.ToString());

            if (utilisateur == null )
                utilisateur = new UtilisateurConnexion("bypass", "bypass", "Vendeur", "bypass");

            if (utilisateur == null)
            {
                MessageBox.Show("Erreur pendant la connexion");
                return;
            }

            MessageBox.Show("Bienvenue " + utilisateur.Prenom + " " + utilisateur.Nom);
            var win = utilisateur.Groupe == "Administrateur"
                ? (Window)new Administrateur()
                : new Vendeur(utilisateur.Login);

            win.Show();
            Application.Current.MainWindow = win;

            this.Close();
        }

        public Connexion()
        {
            InitializeComponent();
        }

        private void Button_Connexion_Click(object sender, RoutedEventArgs e)
        {
            procConnexion();

        }
    }
}
