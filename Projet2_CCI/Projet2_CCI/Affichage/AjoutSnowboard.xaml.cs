using Projet2_CCI.DAL;
using System;
using System.Collections.Generic;
using System.Windows;
namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour AjoutSnowboard.xaml
    /// </summary>
    public partial class AjoutSnowboard : Window
    {
        public AjoutSnowboard()
        {
            List<string> listStyle = new List<string>();
            listStyle = RequeteSqlStyle.sqlReadStyle();

            InitializeComponent();
            this.cbStyle.ItemsSource = listStyle;
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BoutonValider_Click(object sender, RoutedEventArgs e)
        {
           // string nomSnowboard = this.nomSnowboard.Text;
           // string marqueSnowboard = this.marqueSnowboard.Text;
           // string genreSnowboard = this.genreSnowboard.Text;
           // string niveauSnowboard = this.niveauSnowboard.Text;
           // string styleSnowboard = this.styleSnowboard.Text;
            try {
                decimal prixEuroSnowboard = decimal.Parse(this.prixEuroSnowboard.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Merci de renseigner un prix en chiffre dans le prix en Euro");
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Merci de renseigner une valeur dans le prix en Euro");
            }
            try
            {
                decimal prixDollarSnowboard = decimal.Parse(this.prixDollarSnowboard.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Merci de renseigner un prix en chiffre dans le prix en Dollar");
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Merci de renseigner une valeur dans le prix en Dollar");

            }
            try
            {
                int stockSnowboard = int.Parse(this.stockSnowboard.Text);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("merci de renseigner une valeur dans stock");
            }
            catch (FormatException)
            {
                MessageBox.Show("merci de renseigner une valeur correcte dans Stock");
            }
        }
    }
}
