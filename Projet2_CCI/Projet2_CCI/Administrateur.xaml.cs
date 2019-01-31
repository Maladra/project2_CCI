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
        ObservableCollection<string> stringListe = new ObservableCollection<string>();
        List<string> roleListe = new List<string>();

        public Administrateur()
        {
            InitializeComponent();
            // CREATION OBJET USER
            Employe employe1 = new Employe("gnu", "aaa","Administrateur");
            // POPULATE LISTE
            stringListe.Add("aaa");
            stringListe.Add("bbb");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ddd");
            stringListe.Add("ddd");
            stringListe.Add(employe1.Nom);
            roleListe.Add("Vendeur");
            roleListe.Add("Administrateur");

            // COMMUNICATION AVEC INTERFACE
            this.listeUtilisateurs.ItemsSource = stringListe;
            this.ComboGroupeUtilisateur.ItemsSource = roleListe;

        }
        // EVENT BUTTON CLICK
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // RECUPERE VALUE SELECTED
            string ma_value = this.listeUtilisateurs.SelectedItem.ToString();

            // DISPLAY VALUE IN TEXT
            this.test.Text = ma_value;

            // DISPLAY INDEX SELECTED ITEM
            MessageBox.Show(this.listeUtilisateurs.SelectedIndex.ToString());
            // REMOVE ITEM 
            stringListe.RemoveAt(this.listeUtilisateurs.SelectedIndex);           
        }

        private void ListeUtilisateurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object username = this.listeUtilisateurs.SelectedItem;
            Console.WriteLine(username);
            if (username == null)
            {
                this.test.Text = "";
            }
            else
            {
                this.test.Text = username.ToString();
            }
        }


    }
}
