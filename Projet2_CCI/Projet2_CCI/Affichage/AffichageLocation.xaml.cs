using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Models;
using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;
using static Projet2_CCI.LocationAvecId;

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour AffichageLocation.xaml
    /// </summary>

    public class ViewModelAffichageLocation : ViewModelBase
    {
        public ObservableCollection<ClientRequete> Clients { get; } = new ObservableCollection<ClientRequete>();

        public ViewModelAffichageLocation(List<ClientRequete> listeClient)
        {
            this.Clients = new ObservableCollection<ClientRequete>(
                listeClient.Select(client => new ClientRequete(client.IdClient, client.Nom, client.Prenom, client.NumeroTelephone)));

        }


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


        void OnLoad (object sender, RoutedEventArgs e)
        {
            listeClient.ItemsSource = ViewModel.Clients;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.listeClient.ItemsSource);
            view.Filter = UserFilter;
        }


        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as ClientRequete).Nom.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listeClient.ItemsSource).Refresh();
        }











        private void ListBoxListeClientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ChargeListeLocation();
            if (ViewModel.clientSelectione != null)
            {
                this.numeroClient.Content = ViewModel.clientSelectione.NumeroTelephone;
            }
            else
            {
                this.numeroClient.Content = string.Empty;
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
                this.numeroCommande.Content = ViewModel.LocationS.IdLocation;
            }
            else
            {
                this.etatCommande.SelectedItem = string.Empty;
                this.numeroCommande.Content = string.Empty;
            }

        }

        private void Valider_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ViewModel.LocationS != null)
            {
                var listePlancheLouee = RequeteSqlLocation.SelectPlancheLouee(ViewModel.LocationS.IdLocation);

                if (this.etatCommande.Text == "Rendu" && ViewModel.LocationS.EtatLocation != "Rendu")
                {
                    ViewModel.LocationS.EtatLocation = this.etatCommande.Text;

                    foreach (var plancheLocation in listePlancheLouee)
                    {
                        {
                            PlancheLouee PlancheStock = RequeteSqlLocation.SelectPlancheStock(plancheLocation);
                            int stock = plancheLocation.Quantite + PlancheStock.Quantite;
                            RequeteSqlLocation.UpdateStock(plancheLocation.IdPlanche, stock);
                        }
                    }
                    RequeteSqlLocation.updateEtatLocation(ViewModel.LocationS.IdLocation, this.etatCommande.Text);
                    MessageBox.Show("Etat de la location mis à jour");
                }
                else
                {
                    MessageBox.Show("Commande déja rendue");
                }
            }
        }
    }
}

