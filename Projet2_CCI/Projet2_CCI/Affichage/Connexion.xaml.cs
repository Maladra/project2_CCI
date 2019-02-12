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
        public Connexion()
        {
            InitializeComponent();
        }

        private void Button_Connexion_Click(object sender, RoutedEventArgs e)
        {
            HashingPassword test= new HashingPassword();
            Console.WriteLine(test.HashPassword("aaa"));
            
            Vendeur Vendeur = new Vendeur();
            Administrateur Administrateur = new Administrateur();            
            string[] testValue = SQLHelper.SQLiteConnexion(this.UsernameText.Text, this.PasswordText.Password.ToString());
            if (testValue[0] != "erreur")
            {
                this.Close();
                MessageBox.Show("Bienvenue " + testValue[0]+" "+testValue[1]);
                if (testValue[2] =="Administrateur" )
                {
                    Administrateur.Show();
                }
                else
                {
                    Vendeur.Show();
                }
            }
            else
            {
                MessageBox.Show("Erreur pendant la connexion");
            }
        }
    }
}
