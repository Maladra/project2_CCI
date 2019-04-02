using Projet2_CCI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour AjoutSnowboard.xaml
    /// </summary>
    public partial class AjoutSnowboard : Window
    {
        /// <summary>
        /// Ajoute un snowboard dans la base de donnee
        /// </summary>
        // TODO : REFAIRE AVEC UN SEUL POP-UP EN FONCTION DES CAS 
        // TODO : egalement try (2 chiffres apres la virgule pour prix € et $ ainsi que stock en int dans DB) 
        // TODO : FAIRE LA MEME POUR L'AFFICHAGE DU PRIX
        private void addSnowboard()
        {
            string nomSnowboard = this.nomSnowboard.Text;
            string marqueSnowboard = this.cbMarque.SelectedValue.ToString();
            string genreSnowboard = this.cbGenre.SelectedValue.ToString();
            string niveauSnowboard = this.cbNiveau.SelectedValue.ToString();
            string styleSnowboard = this.cbStyle.SelectedValue.ToString();


            try
            {
                decimal prixEuroSnowboard = decimal.Parse(this.prixEuroSnowboard.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Merci de renseigner un prix en chiffre dans le prix en Euro");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Merci de renseigner une valeur dans le prix en Euro");
            }
            try
            {
                decimal prixDollarSnowboard = decimal.Parse(this.prixDollarSnowboard.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Merci de renseigner un prix en chiffre dans le prix en Dollar");
            }
            catch (ArgumentNullException)
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

        public AjoutSnowboard()
        {
            
            List<Donnee.Style> listStyle = new List<Donnee.Style>();
            List<string> listMarque = new List<string>();
            List<string> listGenre = new List<string>();
            List<string> listNiveau = new List<string>();
            
            listStyle = RequeteSqlStyle.SqlReadStyle();
            listMarque = RequeteSqlMarque.SqlReadMarque();
            listGenre = RequeteSqlGenre.SqlReadGenre();
            listNiveau = RequeteSqlNiveau.SqlReadNiveau();

            listMarque.Sort();
            listGenre.Sort();

            InitializeComponent();
            this.cbStyle.ItemsSource = listStyle;           
            this.cbMarque.ItemsSource = listMarque;
            this.cbGenre.ItemsSource = listGenre;
            this.cbNiveau.ItemsSource = listNiveau;
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BoutonValider_Click(object sender, RoutedEventArgs e)
        {

            addSnowboard();

        }
    }
}
