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

    public class DynamicStockSnowboard : ViewModelBase
    {
        private int stock;
        public string Nom { get; }
        public DynamicStockSnowboard(DynamicStockSnowboard snowboard)
        {
            this.Nom = snowboard.Nom;
            this.Stock = snowboard.Stock;
        }
        public DynamicStockSnowboard(SnowboardRequete snowboard)
        {
            this.Nom = snowboard.Nom;
            this.Stock = snowboard.Stock;
        }
        public int Stock
        {
            get { return this.stock; }
            set { this.stock = value; this.OnPropertyChange(); }
        }
    }

    public class SelectionLocationViewModel : ViewModelBase
    {
        public ObservableCollection<DynamicStockSnowboard> StockTempSnowboard { get; }
        public ObservableCollection<DynamicStockSnowboard> LocationListe { get; }

        public SelectionLocationViewModel(IEnumerable<SnowboardRequete> snowboards)
        {
            this.StockTempSnowboard = new ObservableCollection<DynamicStockSnowboard>(
                snowboards.Select(snowboard => new DynamicStockSnowboard(snowboard)));
            this.LocationListe = new ObservableCollection<DynamicStockSnowboard>();
        }
        public void AjouterSnowboard(DynamicStockSnowboard snowboard)
        {
            snowboard.Stock--;

            DynamicStockSnowboard snowboardPresentLocation = LocationListe.Where(s => ReferenceEquals(s, snowboard)).SingleOrDefault();

            if (snowboardPresentLocation == null)
            {
                var selectedSnowboar = new DynamicStockSnowboard(snowboard);
                this.LocationListe.Add(selectedSnowboar);
            }
            else
            {
                var selectedSnowboar = new DynamicStockSnowboard(snowboardPresentLocation);
                
                selectedSnowboar.Stock++;
            }
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
            var row = (DataGridRow)sender;
            this.ViewModel.AjouterSnowboard((DynamicStockSnowboard)row.DataContext);
        }

    }
}
