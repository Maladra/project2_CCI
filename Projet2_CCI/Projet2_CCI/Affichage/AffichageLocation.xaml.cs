using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Models;
using Projet2_CCI.DAL;
using static Projet2_CCI.LocationAvecId;

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour AffichageLocation.xaml
    /// </summary>

    public class ViewModelAffichageLocation : ViewModelBase
    {
        public ObservableCollection<ClientRequete> Clients { get; } =
            new ObservableCollection<ClientRequete>();
        public ObservableCollection<DynamicLocationId> Location { get; } =
            new ObservableCollection<DynamicLocationId>();

        public ClientRequete clientSelectione;
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

        DynamicLocationId locationS;
        public DynamicLocationId LocationS
        {
            get { return locationS; }
            set
            {
                locationS = value;
                this.OnPropertyChange();
                // faire quelque chose
            }
        }
    }



    public partial class AffichageLocation : UserControl
    {
        ViewModelAffichageLocation ViewModel => (ViewModelAffichageLocation)this.DataContext;

        public AffichageLocation()
        {
            InitializeComponent();
            List <string> etatPossible = new List<string>();
            etatPossible.Add("Rendu");
            etatPossible.Add("Non rendu");
            this.etatCommande.ItemsSource = etatPossible;
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
                this.Test.Content = ViewModel.LocationS.EtatLocation;
                this.etatCommande.SelectedItem = ViewModel.LocationS.EtatLocation;
            }

        }
    }
}
