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

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        string _login;
        public ChangePassword(string login)
        {
            InitializeComponent();
            _login = login;
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (this.passwordInput.Password != null && this.passwordInputRepeat.Password != null 
                && this.passwordInput.Password == this.passwordInputRepeat.Password)
            {
                if (RequeteSqlUser.SQLiteChangePassword(_login, this.passwordInput.Password))
                {
                    MessageBox.Show("Mot de passe bien mis à jour.");
                }

            }
            else
            {
                MessageBox.Show("Veuillez vérifier les mots de passe rentrés.");
            }
        }

    }
}
