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
    /// Logique d'interaction pour AjoutStyle.xaml
    /// </summary>
    public partial class AjoutStyle : Window
    {
        public AjoutStyle()
        {
            InitializeComponent();
        }

        private void Button_Valider_Click(object sender, RoutedEventArgs e)
        {

            if (DAL.RequeteSqlStyle.SQLiteAddStyle(this.nomStyle.Text) == true)
            {
                MessageBox.Show("Style bien ajouté dans BD");
                this.Close();
            }
            else
            {
                MessageBox.Show("Style déja present dans BD");
            }
        }

        /// <summary>
        /// ANNULE ET FERME LA FENETRE
        /// </summary>
        private void Button_Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
