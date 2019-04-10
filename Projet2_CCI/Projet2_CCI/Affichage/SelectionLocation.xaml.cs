using Projet2_CCI.Donnee;
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

namespace Projet2_CCI.Affichage
{

    public class DynamicStcokSnowboard : ViewModelBase
    {

        private int stock;

        public int Stock
        {
            get { return this.stock; }
            set { this.stock = value; this.OnPropertyChange(); }
        }
    }

    public class SelectionLocationViewModel : ViewModelBase
    {
        public ObservableCollection<Donnee.SnowboardRequete> StockTempSnowboard { get; }
        public ObservableCollection<Donnee.SnowboardRequete> LocationListe { get; }

        public SelectionLocationViewModel(IEnumerable<SnowboardRequete> snowboards)
        {
            this.StockTempSnowboard = new ObservableCollection<SnowboardRequete>(
                snowboards.Select(snowboard => snowboard.Clone()));
            this.LocationListe = new ObservableCollection<SnowboardRequete>();
        }
        public void AjouterSnowboard(SnowboardRequete snowboard)
        {

            this.LocationListe.Add(snowboard);
        }
        public void Valider()
        {
        }
    }
    /// <summary>
    /// Logique d'interaction pour SelectionLocation.xaml
    /// </summary>
    public partial class SelectionLocation : UserControl
    {
        SelectionLocationViewModel ViewModel => (SelectionLocationViewModel)this.DataContext;
        public SelectionLocation()
        {
            InitializeComponent();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
            => this.ViewModel.Valider();

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.AjouterSnowboard((SnowboardRequete)this.stockSnowboard.SelectedItem);
        }


    }
}
