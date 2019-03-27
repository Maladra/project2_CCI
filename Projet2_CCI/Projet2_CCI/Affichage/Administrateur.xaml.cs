using Projet2_CCI.Affichage;
using Projet2_CCI.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Administrateur.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        // Liste d'utilisateur pour affichage
        ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();
        /// <summary>
        /// Charge la liste d'utilisateur depuis la BD et refresh l'interface
        /// </summary>
        private void loadUserList()
        {
            usersList = RequeteSqlUser.SQLiteListUsers();

            // COMMUNICATION AVEC INTERFACE
            this.listeUtilisateurs.ItemsSource = usersList;
        }


        public Administrateur()
        {
            InitializeComponent();
            loadUserList();

        }

        /// <summary>
        /// Fonction pour l'ajout d'utilisateur
        /// </summary>
        private void Button_AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            AjoutUtilisateur ajoutUtilisateur = new AjoutUtilisateur();
            ajoutUtilisateur.ShowDialog();
            loadUserList();
        }
       
        /// <summary>
        /// Supprime l'utilisateur selectionne
        /// </summary>
        private void Button_SupprimerUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (Employe)this.listeUtilisateurs.SelectedItem;
            if (user != null)
            {
                // Message Box de suppression de l'utilisateur selectionné
                var res = MessageBox.Show(this, "Confirmation suppression utilisateur", "Confirmation suppression", MessageBoxButton.OKCancel,
                MessageBoxImage.Question);


                
                if (res == MessageBoxResult.OK)
                {
                    MessageBox.Show(RequeteSqlUser.SQLiteDeleteUser(user));
                    loadUserList();
                    
                }
                else
                { 
                    
                }
            }
            else
            {
                MessageBox.Show("Merci de selectionner un utilisateur");
            }

        }

        /// <summary>
        /// Edition de l'utilisateur selectionne
        /// </summary>
        private void Button_EditerUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            var user = (Employe)this.listeUtilisateurs.SelectedItem;
            if (user !=null)
            {
                EditUtilisateur editUtilisateur = new EditUtilisateur(user);
                editUtilisateur.ShowDialog();
                loadUserList();

            }
            else
            {
                MessageBox.Show("Merci de selectionner un utilisateur");
            }
        }

    }
}
