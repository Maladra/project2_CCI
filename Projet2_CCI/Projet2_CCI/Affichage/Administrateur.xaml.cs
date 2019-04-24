using Projet2_CCI.Affichage;
using Projet2_CCI.DAL;
using System.Collections.ObjectModel;
using System.Windows;

namespace Projet2_CCI
{

    /// <summary>
    /// Logique d'interaction pour Administrateur.xaml
    /// </summary>
    public partial class Administrateur : Window
    {

        ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();

        /// <summary>
        /// Charge la liste d'utilisateur depuis la BD et refresh l'interface
        /// </summary>
        private void loadUserList()
        {
            usersList = RequeteSqlUser.SQLiteListUsers();
            this.listeUtilisateurs.ItemsSource = usersList;
        }

        /// <summary>
        /// Prend un Employe en parametre et declenche sa suppression
        /// </summary>
        private void deleteUser(Employe user)
        {

            if (user != null)
            {
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
            deleteUser(user);
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
        /// <summary>
        /// Ouvre fenêtre de configuration pour le chemin de la DB
        /// </summary>
        private void BouttonDbClick(object sender, RoutedEventArgs e)
        {
            ChangePathDb changePathDb = new ChangePathDb();
            changePathDb.ShowDialog();

        }
    }
}
