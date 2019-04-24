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

namespace Projet2_CCI.Affichage
{
    /// <summary>
    /// Logique d'interaction pour ChangePathDb.xaml
    /// </summary>
    public partial class ChangePathDb : Window
    {

        public ChangePathDb()
        {
            InitializeComponent();
            this.dbPath.Text = ConfigurationManager.AppSettings["connectionString"];
        }


        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string oldConfigue = ConfigurationManager.AppSettings["connectionString"];
            ConfigurationManager.AppSettings["connectionString"] = this.dbPath.Text;

            string testConnection = DAL.TestDb.ServerConnected();

            if (testConnection != "Connexion établie")
            {

                ConfigurationManager.AppSettings["connectionString"] = oldConfigue;
                MessageBox.Show(testConnection);        
            }
            else
            {
                config.AppSettings.Settings["connectionString"].Value = this.dbPath.Text;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show(testConnection);
                this.Close();
            }

        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
