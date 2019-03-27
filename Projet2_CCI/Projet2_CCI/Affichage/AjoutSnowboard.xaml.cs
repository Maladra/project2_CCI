using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour AjoutSnowboard.xaml
    /// </summary>
    public partial class AjoutSnowboard : Window
    {
        public AjoutSnowboard()
        {
            InitializeComponent();
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BoutonValider_Click(object sender, RoutedEventArgs e)
        {
            string nomSnowboard = this.nomSnowboard.Text;
            string marqueSnowboard = this.marqueSnowboard.Text;
            string genreSnowboard = this.genreSnowboard.Text;
            string niveauSnowboard = this.niveauSnowboard.Text;
            string styleSnowboard = this.styleSnowboard.Text;
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
