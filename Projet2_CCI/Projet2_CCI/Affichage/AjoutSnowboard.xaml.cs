using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private void addSnowboard()
        {
            string nomSnowboard = this.nomSnowboard.Text;
            Marque marqueSnowboard = (Marque)this.cbMarque.SelectedValue;
            Genre genreSnowboard = (Genre)this.cbGenre.SelectedValue;
            Niveau niveauSnowboard = (Niveau)this.cbNiveau.SelectedValue;
            Donnee.Style styleSnowboard = (Donnee.Style)this.cbStyle.SelectedValue;
            bool verificationForm = true;
            StringBuilder erreurFormulaire = new StringBuilder();

            string txtPrixEuroSnowboard = this.prixEuroSnowboard.Text;
            string txtPrixDollarSnowoard = this.prixDollarSnowboard.Text;
            string txtStockSnowboard = this.stockSnowboard.Text;

            decimal prixEuroSnowboard = default(decimal);
            decimal prixDollarSnowboard = default(decimal);
            decimal prixEuroSnowboardRounded = default(decimal);
            decimal prixDollarSnowboardRounded = default(decimal);
            int stockSnowboard = default(int);

            // Test le nom 
            if (string.IsNullOrWhiteSpace(nomSnowboard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner une valeur dans le Nom\n");
            }

            // Test la Marque
            if (marqueSnowboard == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de selectionner une valeur dans la Marque\n");
            }

            // Test le Genre
            if (genreSnowboard == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de selectionner une valeur dans le Genre\n");
            }

            // Test le Niveau
            if (niveauSnowboard == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de selectionner une valeur dans le Niveau\n");
            }

            // Test le Style
            if (styleSnowboard == null)
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de selectionner une valeur dans le Style\n");
            }

            // Test le prix en euro du Snowboard
            if (string.IsNullOrWhiteSpace(txtPrixEuroSnowboard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner une valeur dans le prix en Euro\n");                
            }
            else if (!decimal.TryParse(txtPrixEuroSnowboard, out prixEuroSnowboard)) {

                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner un prix en chiffre dans le prix en Euro\n");
                
            }
            else
            {
                prixEuroSnowboardRounded = decimal.Round(prixEuroSnowboard, 2);
                if (prixEuroSnowboard != prixEuroSnowboardRounded)
                {
                    verificationForm = false;
                    erreurFormulaire.Append("- Merci de renseigner un prix en Euro avec 2 chiffres après la virgule\n");
                }
            }
   
            // Test le prix en dollar du Snowboard
            if (string.IsNullOrWhiteSpace(txtPrixDollarSnowoard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner une valeur dans le prix en Dollar\n");
            }
            else if (!decimal.TryParse(txtPrixDollarSnowoard, out prixDollarSnowboard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner un prix en chiffre dans le prix en Dollar\n");
            }
            else
            {
                prixDollarSnowboardRounded = decimal.Round(prixDollarSnowboard, 2);
                if (prixDollarSnowboardRounded != prixDollarSnowboard )
                {
                    verificationForm = false;
                    erreurFormulaire.Append("- Merci de renseigner un prix en Dollar avec 2 chiffres après la virgule\n");
                }
            }

            // Test le Stock du Snowboard
            if (string.IsNullOrWhiteSpace(txtStockSnowboard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner une valeur dans stock\n");
            }
            else if (!int.TryParse(txtStockSnowboard, out stockSnowboard))
            {
                verificationForm = false;
                erreurFormulaire.Append("- Merci de renseigner une valeur correcte dans Stock\n");
            }

            if (verificationForm)
            {
                int prixEuroFinal = Convert.ToInt32(prixEuroSnowboardRounded * 100);
                int prixDollarFinal = Convert.ToInt32(prixDollarSnowboardRounded * 100);
                SnowboardRequete snowboard = new SnowboardRequete(nomSnowboard,
                    marqueSnowboard, genreSnowboard,
                    niveauSnowboard, styleSnowboard,
                    prixEuroFinal, prixDollarFinal, stockSnowboard);
                RequeteSqlSnowboard.SQLAddSnowboard(snowboard);
                MessageBox.Show("Le snowboard à été rajouté");
                
            }
            else
            {
                MessageBox.Show(erreurFormulaire.ToString());             
            }

        }

        public AjoutSnowboard()
        {
            
            List<Donnee.Style> listStyle = new List<Donnee.Style>();
            List<Marque> listMarque = new List<Marque>();
            List<Genre> listGenre = new List<Genre>();
            List<Niveau> listNiveau = new List<Niveau>();
            
            listStyle = RequeteSqlStyle.SqlReadStyle();
            listMarque = RequeteSqlMarque.SqlReadMarque();
            listGenre = RequeteSqlGenre.SqlReadGenre();
            listNiveau = RequeteSqlNiveau.SqlReadNiveau();

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
