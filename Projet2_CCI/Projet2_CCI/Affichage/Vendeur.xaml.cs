using Projet2_CCI.Affichage;
using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
namespace Projet2_CCI.Affichage
{



    /// <summary>
    /// Logique d'interaction pour Vendeur.xaml
    /// </summary>
    public partial class Vendeur : Window
    {

        ObservableCollection<Donnee.SnowboardRequeteId> snowboardListe = RequeteSqlSnowboard.SQLitePlancheRead();
        private void loadSnowboardList()
        {
            this.snowboardListe = RequeteSqlSnowboard.SQLitePlancheRead();
            this.stockAffichage.ItemsSource = snowboardListe;
        }

        string _login = string.Empty;
        public Vendeur(string login)
        {
            InitializeComponent();
            this.selectionLocation.hourPicker.ItemsSource = Enumerable.Range(0, 24);
            this.selectionLocation.minutePicker.ItemsSource = Enumerable.Range(0, 60);
            this.selectionLocation.moyenPaiement.ItemsSource = new string[] { "Carte Bleue", "Espèce", "Chèque" };
            this.selectionLocation.dateDebut.DisplayDateStart = DateTime.UtcNow;
            this.selectionLocation.dateFin.DisplayDateStart = DateTime.UtcNow.AddDays(1);


            this.affichageLocation.DataContext = new ViewModelAffichageLocation();
            this.stockAffichage.ItemsSource = snowboardListe;
            _login = login;

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
            // TODO: SI deja presente ajoute au Stock plutôt que nouvelle planche
            AjoutSnowboard ajoutSnowboard = new AjoutSnowboard();
            ajoutSnowboard.ShowDialog();
            loadSnowboardList();
        }

        private void ButtonPassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(_login);
            changePassword.ShowDialog();
        }



        private void stockMaterielSelected (object sender, RoutedEventArgs e)
        {
            loadSnowboardList();
        }

        private void locationEncoursSelected(object sender, RoutedEventArgs e)
        {
            List<ClientRequete> clientList = DAL.RequeteSqlClient.SQLiteListClient();
            affichageLocation.listeClient.ItemsSource = clientList;
        }


        private void creationLocationSelected(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Donnee.SnowboardRequeteId> snowboardListe = RequeteSqlSnowboard.SQLitePlancheRead();
            this.selectionLocation.DataContext = new SelectionLocationViewModel(snowboardListe);
        }
    }
}
