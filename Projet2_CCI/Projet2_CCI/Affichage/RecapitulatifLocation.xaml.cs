using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour RecapitulatifLocation.xaml
    /// </summary>
    public partial class RecapitulatifLocation : Window
    {
        public RecapitulatifLocation(string nomClient, string prenomClient, decimal prixTotalEuroHt,
            decimal prixTotalDollarHt, decimal prixEuroTotal,decimal prixDollarTotal,
            decimal tva, DateTime dateDebut, DateTime dateFin,
            List<DynamicStockSnowboard> listLocation, int duree)
        {
            string TotalEuroHtFormated = prixTotalEuroHt.ToString("C", CultureInfo.CreateSpecificCulture("fr-FR"));
            string TotalEuroFormated = prixEuroTotal.ToString("C", CultureInfo.CreateSpecificCulture("fr-FR"));
            string TotalDollarHtFormated = prixTotalDollarHt.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            InitializeComponent();
            this.listeLocation.ItemsSource = listLocation;
            this.nomClient.Content = nomClient;
            this.prenomClient.Content = prenomClient;
            this.prixTotalEuroHt.Content = TotalEuroHtFormated;
            this.dureeLocation.Content = Pluriel("un jour", "{0} jours", duree, duree);
            this.tva.Content = tva + " %";
            this.dateDebut.Content = dateDebut.ToLongDateString();
            this.dateFin.Content = dateFin.ToLongDateString();
            this.prixTotalEuro.Content = TotalEuroFormated;

        }
        static string Pluriel(string singulier, string pluriel, long n, params object[] fmt)
            => string.Format(
                n == 1 ? singulier : pluriel,
                fmt);
    }
}
