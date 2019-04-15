using Projet2_CCI.DAL;
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

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour EditUtilisateur.xaml
    /// </summary>
    public partial class EditUtilisateur : Window
    {
        Employe userBefore;
        // TODO : EDITION UTILISATEUR ET VERIFICATION DES CHAMPS
        public EditUtilisateur(Employe user)
        {
            this.userBefore = user;
            InitializeComponent();
            this.loginUtilisateur.Text = user.Login;
            this.nomUtilisateur.Text = user.Nom;
            this.prenomUtilisateur.Text = user.Prenom;
            this.groupeUtilisateur.Text = user.Groupe;
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            Employe userAfter = new Employe(this.nomUtilisateur.Text, this.nomUtilisateur.Text, this.loginUtilisateur.Text, this.passwordUtilisateur.Text, this.groupeUtilisateur.Text);
            try
            {
                RequeteSqlUser.SQLiteEditUser(this.userBefore, userAfter);
                MessageBox.Show("Utilisateur édité.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur pendant l'édition de l'utilisateur : " + ex.Message);
            }

        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
