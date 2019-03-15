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
    /// Logique d'interaction pour EditUtilisateur.xaml
    /// </summary>
    public partial class EditUtilisateur : Window
    {
        public EditUtilisateur(string nomUser, string prenomUser, string userLogin,string userGroupe)
        {
            InitializeComponent();
            this.loginUtilisateur.Text = userLogin;
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
