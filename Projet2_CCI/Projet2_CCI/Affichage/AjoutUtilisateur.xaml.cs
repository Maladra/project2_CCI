﻿using Projet2_CCI.Utils;
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
    /// Logique d'interaction pour AjoutUtilisateur.xaml
    /// </summary>
    public partial class AjoutUtilisateur : Window
    {
        public AjoutUtilisateur()
        {
            InitializeComponent();
            this.listeGroupe.Items.Add("Vendeur");
            this.listeGroupe.Items.Add("Administrateur");
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///  VERIFIE LES CHAMPS ET INSERT USER DANS LA BD SI all champ sont valides
        /// </summary>
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {

            string nomEmploye = nom.Text;          
            string prenomEmploye = prenom.Text;
            string loginEmploye = login.Text;
            string passwordEmploye = password.Text;
            string groupeEmploye = listeGroupe.Text;
            if (UtilsClass.VerifString(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye))
            {

                Employe employe = new Employe(nom.Text, prenom.Text, login.Text, password.Text, listeGroupe.Text);
                if (SQLHelper.SQLiteAddUser(employe))
                {
                    MessageBox.Show("L'utilisateur a été crée");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur pendant la requête");
                }

            }
            else
            {
                MessageBox.Show("Merci de renseigner tous les champs");
            }
        }

    }
}