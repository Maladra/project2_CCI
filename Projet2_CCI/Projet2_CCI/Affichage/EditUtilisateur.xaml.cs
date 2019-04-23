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

        public void editUser()
        {
            bool verificationForm = true;
            StringBuilder erreurFormulaire = new StringBuilder();
            erreurFormulaire.AppendLine("Le(s) champs suivants sont vide :");
            if (string.IsNullOrWhiteSpace(this.nomUtilisateur.Text))
            {
                verificationForm = false;
                erreurFormulaire.AppendLine("- Nom d'utilisateur");
            }
            if (string.IsNullOrWhiteSpace(this.prenomUtilisateur.Text))
            {
                verificationForm = false;
                erreurFormulaire.AppendLine("- Prénom d'utilisateur");
            }
            if (string.IsNullOrWhiteSpace(this.loginUtilisateur.Text))
            {
                verificationForm = false;
                erreurFormulaire.AppendLine("- Login de l'utilisateur");
            }

            if (!verificationForm)
            {
                MessageBox.Show(erreurFormulaire.ToString());
            }
            else
            {
                Employe userAfter = new Employe(this.nomUtilisateur.Text, this.prenomUtilisateur.Text, this.loginUtilisateur.Text, this.passwordUtilisateur.Password, this.groupeUtilisateur.Text);
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
        }
        Employe userBefore;
        public EditUtilisateur(Employe user)
        {

            this.userBefore = user;

            InitializeComponent();

            List<string> groupeUtilisateurSelect = new List<string>();
            groupeUtilisateurSelect.Add("Administrateur");
            groupeUtilisateurSelect.Add("Vendeur");
            this.groupeUtilisateur.ItemsSource = groupeUtilisateurSelect;

            this.loginUtilisateur.Text = user.Login;
            this.nomUtilisateur.Text = user.Nom;
            this.prenomUtilisateur.Text = user.Prenom;
            this.groupeUtilisateur.Text = user.Groupe;
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            editUser();
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
