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
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        string connString = "Data Source = D:/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3";

        public Connexion()
        {
            InitializeComponent();

        }

        private void Button_Connexion_Click(object sender, RoutedEventArgs e)
        {
            SQLHelper.SQLiteRead("SELECT * FROM Planche_snowboard;", connString);
            Vendeur Vendeur = new Vendeur();
            Administrateur Administrateur = new Administrateur();
            this.Hide();
            Vendeur.Show();
            Administrateur.Show();
            MessageBox.Show("Erreur pendant la connexion.");
            MessageBox.Show("Connerion réussie \n\r Bienvenue Bidule.");
        }
    }
}
