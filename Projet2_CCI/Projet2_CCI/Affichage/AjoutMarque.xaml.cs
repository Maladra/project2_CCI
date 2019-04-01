using Projet2_CCI.DAL;
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

namespace Projet2_CCI
{
    /// <summary>
    /// Logique d'interaction pour AjoutMarque.xaml
    /// </summary>
    public partial class AjoutMarque : Window
    {
        public AjoutMarque()
        {
            InitializeComponent();
        }


        /// <summary>
        /// AJOUT MARQUE SNOWBOARD DANS BD
        /// </summary>
        private void Button_Valider_Click(object sender, RoutedEventArgs e)
        {
            // TODO: VERIFICATION CHAMP NON VIDE et verif que marque n'existe pas deja
            RequeteSqlMarque.SQLiteAddMarque(this.NomMarque.Text);
            this.Hide();
            MessageBox.Show("Ajout de la marque avec succés");
            
        }

        /// <summary>
        /// ANNULE ET FERME LA FENETRE
        /// </summary>
        private void Button_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
