using Projet2_CCI.Affichage;
using Projet2_CCI.DAL;
using System.Collections.ObjectModel;
using System.Windows;
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
            ObservableCollection<Donnee.SnowboardRequete> snowboardListe = RequeteSqlSnowboard.SQLitePlancheRead();
            this.stockAffichage.ItemsSource = snowboardListe;
        }

        /// <summary>
        /// Déclenche la fenetre d'ajout d'une marque de snowboard
        /// </summary>
        private void Button_ajoutMarque_Click(object sender, RoutedEventArgs e)
        {
            AjoutMarque ajoutMarque = new AjoutMarque();
            ajoutMarque.ShowDialog();
        }

        /// <summary>
        /// Déclenche la fenetre d'ajout d'un style de snowboard
        /// </summary>
        private void Button_ajoutStyle_Click(object sender, RoutedEventArgs e)
        {
            AjoutStyle ajoutStyle = new AjoutStyle();
            ajoutStyle.ShowDialog();
        }

        /// <summary>
        /// Déclenche la fenetre d'ajout de snowboard
        /// </summary>
        private void Button_ajoutSnowboard_Click(object sender, RoutedEventArgs e)
        {
            AjoutSnowboard ajoutSnowboard = new AjoutSnowboard();
            ajoutSnowboard.ShowDialog();
        }
        // INUTILE ???
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        { // TODO: VERIFIER UTILITE DE CETTE FONCTION (BIND)
            MessageBox.Show("Erreur pendant la création de la location");
        }
    }
}
