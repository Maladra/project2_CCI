using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Models;
using Projet2_CCI.DAL;
namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour AffichageLocation.xaml
    /// </summary>

    public class ViewModelAffichageLocation : ViewModelBase
    {
        public ObservableCollection<ClientRequete> Clients { get; } = new ObservableCollection<ClientRequete>();
        public ObservableCollection<Location> Location { get; } = new ObservableCollection<Location>();


        public bool EtatLocation { get; set; }

        Location locationS;
        public Location LocationS
        {
            get { return locationS; }
            set
            {
                locationS = value;
                this.OnPropertyChange();
            }
        }
        ClientRequete clientSelectione;
        public ViewModelAffichageLocation()
        {
            var a = this.LocationS;
        }

        public ClientRequete ClientSelectione
        {
            get { return clientSelectione; }
            set
            {
                clientSelectione = value;
                this.OnPropertyChange();
                this.ChargeListeLocation();
            }
        }

        public void ChargeListeLocation()
        {
            Location.Clear();
            var newLocations = DAL.RequeteSqlLocation.listLocationSnowboard(ClientSelectione.IdClient);
            foreach (var i in newLocations)
                Location.Add(i);
        }

        public void etatLocation(Location location)
        {
            
        }
    }



    public partial class AffichageLocation : UserControl
    {
        ViewModelAffichageLocation ViewModel => (ViewModelAffichageLocation)this.DataContext;

        public AffichageLocation()
        {
            InitializeComponent();
        }

        private void ListBoxListeClientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ChargeListeLocation();
        }

        private void EtatCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListeLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listeLocation.SelectedItem != null)
            {
                Location location = (Location)listeLocation.SelectedItem;
            }

        }
    }
}
