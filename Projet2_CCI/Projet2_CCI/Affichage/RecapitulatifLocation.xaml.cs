﻿using Models;
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
    /// Logique d'interaction pour RecapitulatifLocation.xaml
    /// </summary>
    public partial class RecapitulatifLocation : Window
    {
        public RecapitulatifLocation(string nomClient, string prenomClient, decimal prixTotalHt, decimal prixTotal,
            decimal tva, DateTime dateDebut, DateTime dateFin, DynamicStockSnowboard listLocation )
        {
            InitializeComponent();
        }
    }
}