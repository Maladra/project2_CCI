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
        public void ajouterStyle()
        {
            if (string.IsNullOrWhiteSpace(this.nomStyle.Text))
            {
                MessageBox.Show("Merci de renseigner un style");
            }
            else { 
                if (DAL.RequeteSqlStyle.SQLiteAddStyle(this.nomStyle.Text) == true)
                {
                    MessageBox.Show("Le style à bien été ajouté");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Le style est déja present");
                }
            }
        }

        public AjoutStyle()
        {
            InitializeComponent();
        }

        private void Button_Valider_Click(object sender, RoutedEventArgs e)
        {
            ajouterStyle();
        }
        private void Button_Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
