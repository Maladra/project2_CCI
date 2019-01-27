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
        ObservableCollection<string> stringListe = new ObservableCollection<string>();
        public Administrateur()
        {
            InitializeComponent();
            stringListe.Add("aaa");
            stringListe.Add("bbb");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");
            stringListe.Add("ccc");

            this.DataContext = stringListe;
            this.listeUtilisateurs.ItemsSource = stringListe;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //stringListe.Add("hhh");
            //stringListe.RemoveAt(1);
            MessageBox.Show(this.listeUtilisateurs.SelectedIndex.ToString());
            stringListe.RemoveAt(this.listeUtilisateurs.SelectedIndex);


        }
    }
}
