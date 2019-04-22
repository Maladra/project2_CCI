using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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
            if (clientSelectione != null)
            {
                var newLocations = DAL.RequeteSqlLocation.listLocationSnowboard(ClientSelectione.IdClient);
                foreach (var i in newLocations)
                    Location.Add(i);
            }

        }

        DynamicLocationId locationS;
        public DynamicLocationId LocationS
        {
            get { return locationS; }
            set
            {
                locationS = value;
                this.OnPropertyChange();
            }
        }
    }



    public partial class AffichageLocation : UserControl
    {
        ViewModelAffichageLocation ViewModel => (ViewModelAffichageLocation)this.DataContext;

        public AffichageLocation()
        {
            InitializeComponent();
            List<string> etatPossible = new List<string>();
            etatPossible.Add("Rendu");
            etatPossible.Add("Non rendu");
            this.etatCommande.ItemsSource = etatPossible;
        }

        private void ListBoxListeClientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ChargeListeLocation();
            if (ViewModel.clientSelectione != null)
            {
                this.numeroClient.Content = ViewModel.clientSelectione.NumeroTelephone;
            }
        }

        private void EtatCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListeLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listeLocation.SelectedItem != null)
            {

                this.etatCommande.SelectedItem = ViewModel.LocationS.EtatLocation;
            }

        }

        private void Valider_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ViewModel.LocationS != null)
            {
                RequeteSqlLocation.updateEtatLocation(ViewModel.LocationS.IdLocation, this.etatCommande.Text);
                MessageBox.Show("Etat de la location mis à jour");
            }
        }
    }
}
