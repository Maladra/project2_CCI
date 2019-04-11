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
        public long Id { get; }
        public string Nom { get; }
        /// <summary>
        /// Construit un object dynamicSnowboard et l'initialise a 0 pour la colone location
        /// </summary>
        public DynamicStockSnowboard(DynamicStockSnowboard snowboard)
        {
            this.Id = snowboard.Id;
            this.Nom = snowboard.Nom;
            this.Stock = 0;
            
        }
        public DynamicStockSnowboard(SnowboardRequeteId snowboard)
        {
            this.Id = snowboard.IdSnowboard;
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

        public SelectionLocationViewModel(IEnumerable<SnowboardRequeteId> snowboards)
        {
            this.StockTempSnowboard = new ObservableCollection<DynamicStockSnowboard>(
                snowboards.Select(snowboard => new DynamicStockSnowboard(snowboard)));
            this.LocationListe = new ObservableCollection<DynamicStockSnowboard>();
        }
        public void AjouterSnowboard(DynamicStockSnowboard snowboard)
        {

            snowboard.Stock--;
            

            if (!this.LocationListe.Any(s =>s.Id == snowboard.Id))
            {
                DynamicStockSnowboard snowboardLocation = new DynamicStockSnowboard(snowboard);
                snowboardLocation.Stock++;
                this.LocationListe.Add(snowboardLocation);
            }
            else
            {
                var snowboardLocation = this.LocationListe
                    .Where(s => s.Id == snowboard.Id)
                    .Single();
                snowboardLocation.Stock++;
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
