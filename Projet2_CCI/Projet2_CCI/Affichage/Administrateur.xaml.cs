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
//using System.Data.SQLite;
//using System.Data.SQLite.Linq;

namespace Projet2_CCI
{

    /// <summary>
    /// Logique d'interaction pour Administrateur.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        // VAR
        ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();
        // COMMUNICATION BD
        private void loadUserList()
        {
            usersList = SQLHelper.SQLiteListUsers();

            // COMMUNICATION AVEC INTERFACE
            this.listeUtilisateurs.ItemsSource = usersList;
        }


        public Administrateur()
        {
            InitializeComponent();
            loadUserList();

        }
        // EVENT BUTTON CLICK

        private void ListeUtilisateurs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var user = (Employe)this.listeUtilisateurs.SelectedItem;
            MessageBox.Show(user.Login);
        }

        private void Button_AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            AjoutUtilisateur ajoutUtilisateur = new AjoutUtilisateur();
            ajoutUtilisateur.ShowDialog();
            loadUserList();
        }
        private void Button_SupprimerUser_Click(object sender, RoutedEventArgs e)
        {  // // RECUPERE VALUE SELECTED
           // string ma_value = this.listeUtilisateurs.SelectedItem.ToString();
           // // DISPLAY VALUE IN TEXT
           // this.test.Text = ma_value;
           // // DISPLAY INDEX SELECTED ITEM
           // MessageBox.Show(this.listeUtilisateurs.SelectedIndex.ToString());
           // // REMOVE ITEM 
           // stringListe.RemoveAt(this.listeUtilisateurs.SelectedIndex);
        }
        private void Button_EditerUtilisateur_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
