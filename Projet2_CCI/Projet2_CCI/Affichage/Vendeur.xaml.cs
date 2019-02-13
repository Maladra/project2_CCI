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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet2_CCI
{
    /// <summary>
    /// Logique d'interaction pour Vendeur.xaml
    /// </summary>
    public partial class Vendeur : Window
    {
        

        public Vendeur()
        {
            InitializeComponent();
            ObservableCollection<SnowboardRequete> snowboardListe = SQLHelper.SQLitePlancheRead();
            this.stockAffichage.ItemsSource = snowboardListe;
        }

        private void Button_ajoutMarque_Click(object sender, RoutedEventArgs e)
        {
            AjoutMarque ajoutMarque = new AjoutMarque();
            ajoutMarque.ShowDialog();
        }

        private void Button_ajoutStyle_Click(object sender, RoutedEventArgs e)
        {
            AjoutStyle ajoutStyle = new AjoutStyle();
            ajoutStyle.ShowDialog();
        }

        private void Button_ajoutSnowboard_Click(object sender, RoutedEventArgs e)
        {
            AjoutSnowboard ajoutSnowboard = new AjoutSnowboard();
            ajoutSnowboard.ShowDialog();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Erreur pendant la création de la location");
        }
    }
}
