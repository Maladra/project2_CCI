﻿using Models;
using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projet2_CCI.Affichage
{

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

        /// <summary>
        /// Si true => ajout de la planche a la location
        /// SI false => fais rien et retourne un message
        /// </summary>
        public bool AjouterSnowboard(DynamicStockSnowboard snowboard)
        {
            if (snowboard.Stock == 0)
            {
                return false;
            }

            snowboard.Stock--;

            if (!this.LocationListe.Any(s => s.Id == snowboard.Id))
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
            return true;
        }

        /// <summary>
        /// Enlève une planche de la liste de location
        /// </summary>
        public void RemoveSnowboard(DynamicStockSnowboard snowboard)
        {
            snowboard.Stock--;

            if (snowboard.Stock == 0)
            {
                this.LocationListe.Remove(snowboard);
            }

            var snowboardStock = this.StockTempSnowboard
                .Where(s => s.Id == snowboard.Id)
                .Single();
            snowboardStock.Stock++;
        }

        /// <summary>
        /// Insert la location dans la DB ainsi que les planches associés avant d'afficher un recapitulatif
        /// </summary>
        public void ValiderLocation(string nomClient, string prenomClient, string NumeroTelephoneClient, string moyenPaiement, decimal tva,
            DateTime debutLocation, DateTime finLocation)
        {
            List<DynamicStockSnowboard> ConvertedListLocation = this.LocationListe.ToList();
            List<DynamicStockSnowboard> ConvertedListStock = this.StockTempSnowboard.ToList();

            var listeSnowboardLocation = ConvertedListLocation.Select(snowboard =>
            new SnowboardRequeteId(snowboard.Id, snowboard.Nom, snowboard.Marque, snowboard.Genre, snowboard.Niveau, snowboard.style,
            snowboard.PrixSnowboardEuro, snowboard.PrixSnowboardDollar, snowboard.Stock));

            var listeSnowboardStock = ConvertedListStock.Select(snowboard =>
            new SnowboardRequeteId(snowboard.Id, snowboard.Nom, snowboard.Marque, snowboard.Genre, snowboard.Niveau,
            snowboard.style, snowboard.PrixSnowboardEuro, snowboard.PrixSnowboardDollar, snowboard.Stock));

            LocationAvecListeSnowboard location = new LocationAvecListeSnowboard(new Client(nomClient, prenomClient, NumeroTelephoneClient)
                , moyenPaiement, debutLocation, finLocation, tva, "Non rendu", listeSnowboardLocation.ToList());

            bool InsertLocation = RequeteSqlLocation.insertLocationSnowboard(location);

            if (InsertLocation)
            {
                var stockUpdated = from loc in listeSnowboardLocation
                                   join s in listeSnowboardStock on loc.IdSnowboard equals s.IdSnowboard
                                   select s;

                foreach (var snowboard in stockUpdated)
                {

                    RequeteSqlLocation.updateStockSnowboard(snowboard.Stock, snowboard.IdSnowboard);
                }
                
                var dureeLocation = (DateTime)finLocation - (DateTime)debutLocation;
                decimal totalLocationEuroHt = ConvertedListLocation.Sum(s => (s.PrixSnowboardEuro * s.Stock) * dureeLocation.Days);
                decimal totalLocationDollarHt = ConvertedListLocation.Sum(s => s.PrixSnowboardDollar * s.Stock * dureeLocation.Days);
                decimal ajoutprixEuroTva = totalLocationEuroHt * (tva / 100);
                decimal ajoutprixDollarTva = totalLocationDollarHt * (tva / 100);

                decimal totalLocationEuro = totalLocationEuroHt + ajoutprixEuroTva;
                decimal totalLocationDollar = totalLocationDollarHt + ajoutprixDollarTva;

                var recapitulatifLocation = new RecapitulatifLocation(nomClient, prenomClient, totalLocationEuroHt, totalLocationDollarHt,
                    totalLocationEuro,totalLocationDollar,tva,finLocation,debutLocation, ConvertedListLocation, dureeLocation.Days);
                recapitulatifLocation.ShowDialog();             
            }
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

        StringBuilder erreurFormulaire = new StringBuilder();
        private bool ValidationForm()
        {
            string txtTva = this.tva.Text;
            decimal tva = default(decimal);
            decimal tvaRounded = default(decimal);

            bool verificationForm = true;
            erreurFormulaire.Append("Merci de renseigner :\n");

            // Test le nom du client
            if (string.IsNullOrWhiteSpace(this.nomClient.Text))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Un nom de client\n");
            }
            // Test le numéro de téléphone du client
            if (string.IsNullOrWhiteSpace(this.numeroTelephoneClient.Text))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Un numéro de téléphone\n");
            }

            // Test le prenom du client
            if (string.IsNullOrWhiteSpace(this.prenomClient.Text))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Un prénom de client\n ");
            }

            // Test le moyen de paiement
            if (string.IsNullOrWhiteSpace(this.moyenPaiement.Text))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Un moyen de paiement\n");
            }
            // Test la date de début
            if (this.dateDebut.SelectedDate == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Une date de debut\n");
            }

            // Test la date de fin
            if (this.dateFin.SelectedDate == null || this.dateFin.SelectedDate < this.dateDebut.SelectedDate)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Une date de fin correcte\n");
            }
            // Test l'heure de debut
            if (this.hourPicker.SelectedItem == null || this.minutePicker.SelectedItem == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Une horraire de début\n");
            }
            // Test la TVA
            if (string.IsNullOrWhiteSpace(txtTva))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Une valeur dans la TVA\n");
            }
            else if (!decimal.TryParse(txtTva, out tva))
            {

                verificationForm = false;
                erreurFormulaire.Append("- Une valeur numérique\n");

            }
            else
            {
                tvaRounded = decimal.Round(tva, 2);
                if (tva != tvaRounded)
                {
                    verificationForm = false;
                    erreurFormulaire.Append("- Une TVA avec 2 chiffres après la virgule\n");
                }
            }
            return verificationForm;

        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationForm())
            {
                this.ViewModel.ValiderLocation(this.nomClient.Text, this.prenomClient.Text, this.numeroTelephoneClient.Text, this.moyenPaiement.Text, Convert.ToDecimal(this.tva.Text),
                                   (DateTime)this.dateDebut.SelectedDate, (DateTime)this.dateFin.SelectedDate);
            }
            else
            {
                MessageBox.Show(erreurFormulaire.ToString());
                erreurFormulaire.Clear();
            }

        }

        private void DataGridStock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            if (!this.ViewModel.AjouterSnowboard((DynamicStockSnowboard)row.DataContext))
            {
                MessageBox.Show("Stock vide");
            }
        }

        private void DataGridLocation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            this.ViewModel.RemoveSnowboard((DynamicStockSnowboard)row.DataContext);
        }

    }

}
