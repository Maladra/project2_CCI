using Projet2_CCI.Affichage;
using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace Projet2_CCI.Affichage
{

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Logique d'interaction pour Vendeur.xaml
    /// </summary>
    public partial class Vendeur : Window
    {
        string _login = string.Empty;
        public Vendeur(string login)
        {
            InitializeComponent();
            ObservableCollection<Donnee.SnowboardRequete> snowboardListe = RequeteSqlSnowboard.SQLitePlancheRead();
            this.selectionLocation.DataContext = new SelectionLocationViewModel(snowboardListe);
            this.stockAffichage.ItemsSource = snowboardListe;
            _login = login;
        }

        private void Button_ajoutMarque_Click(object sender, RoutedEventArgs e)
        {
            AjoutMarque ajoutMarque = new AjoutMarque();
            ajoutMarque.ShowDialog();
        }

        private void Button_ajoutStyle_Click(object sender, RoutedEventArgs e)
        {
            AjoutStyle ajoutStyle = new AjoutStyle();
            ajoutStyle.ShowDialog();
        }

        private void Button_ajoutSnowboard_Click(object sender, RoutedEventArgs e)
        {
            AjoutSnowboard ajoutSnowboard = new AjoutSnowboard();
            ajoutSnowboard.ShowDialog();
        }

        private void ButtonPassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(_login);
            changePassword.ShowDialog();

        }
    }
}
