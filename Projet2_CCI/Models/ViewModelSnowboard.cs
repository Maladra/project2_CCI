using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{


public class ViewModelBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChange([CallerMemberName]string propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }



public class DynamicStockSnowboard : ViewModelBase
        {
            public long Id { get; }
            public string Nom { get; }
            public decimal PrixSnowboardEuro { get; }
            public decimal PrixSnowboardDollar { get; }
            public Genre Genre { get; }
            public Marque Marque { get; }
            public Niveau Niveau { get; }
            public Projet2_CCI.Donnee.Style style { get; }
            public string Etat { get; set; }
            private int stock;

            /// <summary>
            /// Construit un object dynamicSnowboard et l'initialise a 0 pour la colone location
            /// </summary>
            public DynamicStockSnowboard(DynamicStockSnowboard snowboard)
            {
                this.Id = snowboard.Id;
                this.Nom = snowboard.Nom;
                this.PrixSnowboardEuro = snowboard.PrixSnowboardEuro;
                this.PrixSnowboardDollar = snowboard.PrixSnowboardDollar;
                this.Genre = new Genre(snowboard.Genre.Id, snowboard.Genre.Nom);
                this.Marque = new Marque(snowboard.Marque.Id, snowboard.Marque.Nom);
                this.Niveau = new Niveau(snowboard.Niveau.Id, snowboard.Niveau.Nom);
                this.style = new Projet2_CCI.Donnee.Style(snowboard.style.Id, snowboard.style.Nom);
                this.Stock = 0;
            }
            public DynamicStockSnowboard(SnowboardRequeteId snowboard)
            {
                this.Id = snowboard.IdSnowboard;
                this.Nom = snowboard.Nom;
                this.PrixSnowboardEuro = snowboard.PrixEuro;
                this.PrixSnowboardDollar = snowboard.PrixDollar;
                this.Genre = new Genre(snowboard.Genre.Id, snowboard.Genre.Nom);
                this.Marque = new Marque(snowboard.Marque.Id, snowboard.Marque.Nom);
                this.Niveau = new Niveau(snowboard.Niveau.Id, snowboard.Niveau.Nom);
                this.style = new Projet2_CCI.Donnee.Style(snowboard.Style.Id, snowboard.Style.Nom);
                this.Stock = snowboard.Stock;

            }
            public int Stock
            {
                get { return this.stock; }
                set { this.stock = value; this.OnPropertyChange(); }
            }
        }

    }

