using Projet2_CCI.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private void addMarque()
        {
            if (string.IsNullOrWhiteSpace(this.NomMarque.Text))
            {
                MessageBox.Show("Merci de renseigner une marque");
            }
            else
            {
                if (RequeteSqlMarque.SQLiteAddMarque(this.NomMarque.Text))
                {
                    MessageBox.Show("Ajout de la marque avec succès");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Cette marque est déja présente");
                }
            }
        }

        public AjoutMarque()
        {
            InitializeComponent();
        }



        private void Button_Valider_Click(object sender, RoutedEventArgs e)
        {
            addMarque();
            
        }

        private void Button_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
