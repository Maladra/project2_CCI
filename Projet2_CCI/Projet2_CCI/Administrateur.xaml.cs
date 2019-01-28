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


        ObservableCollection<string> stringListe = new ObservableCollection<string>();
        List<string> roleListe = new List<string>();

        public Administrateur()
        {
            InitializeComponent();
            Employe employe1 = new Employe("gnu", "aaa","Administrateur");
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

            this.listeUtilisateurs.ItemsSource = stringListe;
            this.ComboGroupeUtilisateur.ItemsSource = roleListe;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            string ma_value = this.listeUtilisateurs.SelectedItem.ToString();
            this.test.Text = ma_value;
            MessageBox.Show(this.listeUtilisateurs.SelectedIndex.ToString());
            stringListe.RemoveAt(this.listeUtilisateurs.SelectedIndex);
            
        }

        private void ListeUtilisateurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object username = this.listeUtilisateurs.SelectedItem;
            Console.WriteLine(username);
            if (username == null)
            {
                //this.test.Text = username.ToString();
                this.test.Text = "NULL";
            }
            else
            {
                this.test.Text = username.ToString();
            }
        }


    }
}
