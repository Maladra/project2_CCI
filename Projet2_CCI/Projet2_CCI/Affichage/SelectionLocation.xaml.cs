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
        public long Id { get; }
        public string Nom { get; }
        public decimal PrixSnowboardEuro { get; }
        public decimal PrixSnowboardDollar { get; }
        public Genre Genre { get; }
        public Marque Marque { get; }
        public Niveau Niveau { get; }
        public Donnee.Style style { get; }      
        public string Etat { get; set; }
        private int stock;

        /// <summary>
        /// Construit un object dynamicSnowboard et l'initialise a 0 pour la colone location
        /// </summary>
        public DynamicStockSnowboard(DynamicStockSnowboard snowboard)
        {
            this.Id = snowboard.Id;
            this.Nom = snowboard.Nom;
            this.PrixSnowboardEuro = snowboard.PrixSnowboardEuro;
            this.PrixSnowboardDollar = snowboard.PrixSnowboardDollar;
            this.Genre = new Genre(snowboard.Genre.Id, snowboard.Genre.Nom);
            this.Marque = new Marque(snowboard.Marque.Id, snowboard.Marque.Nom);
            this.Niveau = new Niveau(snowboard.Niveau.Id, snowboard.Niveau.Nom);
            this.style = new Donnee.Style(snowboard.style.Id, snowboard.style.Nom);
            this.Stock = 0;
        }
        public DynamicStockSnowboard(SnowboardRequeteId snowboard)
        {
            this.Id = snowboard.IdSnowboard;
            this.Nom = snowboard.Nom;
            this.PrixSnowboardEuro = snowboard.PrixEuro;
            this.PrixSnowboardDollar = snowboard.PrixDollar;
            this.Genre = new Genre(snowboard.Genre.Id, snowboard.Genre.Nom);
            this.Marque = new Marque(snowboard.Marque.Id, snowboard.Marque.Nom);
            this.Niveau = new Niveau(snowboard.Niveau.Id, snowboard.Niveau.Nom);
            this.style = new Donnee.Style(snowboard.Style.Id, snowboard.Style.Nom);
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

        public void ValiderLocation(string nomClient, string prenomClient, string moyenPaiement, decimal tva,
            DateTime debutLocation, DateTime finLocation)
        {
            // utilité ? 
            decimal prixTotalEuroSnowboard = this.LocationListe.Select(prixEuroSnow =>
            prixEuroSnow.PrixSnowboardEuro * prixEuroSnow.Stock).Sum();
            decimal prixTotalDollarSnowboard = this.LocationListe.Select(prixDollarSnow =>
            prixDollarSnow.PrixSnowboardDollar * prixDollarSnow.Stock).Sum();

            List<DynamicStockSnowboard> ConvertedList = this.LocationListe.ToList();
            var listeSnowboardLocation = ConvertedList.Select(snowboard =>
            new SnowboardRequeteId(snowboard.Id,snowboard.Nom, snowboard.Marque, snowboard.Genre, snowboard.Niveau, snowboard.style,
            snowboard.PrixSnowboardEuro, snowboard.PrixSnowboardDollar, snowboard.Stock));


            Location location = new Location(nomClient, prenomClient, moyenPaiement, debutLocation,
                finLocation, listeSnowboardLocation.ToList(), tva, "En cours");
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
            //StringBuilder erreurFormulaire = new StringBuilder();
            erreurFormulaire.Append("Merci de renseigner :\n");

            // Test le nom du client
            if (string.IsNullOrWhiteSpace(this.nomClient.Text))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Un nom de client\n");
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
                // TODO VALIDATION (SQL)
                //Location location = new Location(this.nomClient.Text, this.prenomClient.Text, this.moyenPaiement.Text,  

                this.ViewModel.ValiderLocation(this.nomClient.Text, this.nomClient.Text, this.moyenPaiement.Text, Convert.ToDecimal(this.tva.Text),
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


// TODO : Pouvoir rentrer une value plutôt que Double Click