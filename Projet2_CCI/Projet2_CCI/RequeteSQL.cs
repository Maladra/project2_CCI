﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    // CONTIENT REQUETE SQL
    static class SQLHelper
    {
        // QUERY PLANCHE
        public static ObservableCollection<Snowboard> SQLitePlancheRead()
        {
            string connString = "Data Source = C:/Users/adai101/Desktop/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3"; // CONNECTION STRING
            ObservableCollection<Snowboard> snowboardListe = new ObservableCollection<Snowboard>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Stock,Prix,Niveau,Marque,Genre,Style FROM Planche_snowboard " +
                    "INNER JOIN Niveau_snowboard ON Niveau_snowboard.Id_niveau = Planche_snowboard.Fk_niveau " +
                    "INNER JOIN Marque_snowboard ON Marque_snowboard.Id_marque = Planche_snowboard.Fk_marque " +
                    "INNER JOIN Genre_snowboard ON Genre_snowboard.Id_genre = Planche_snowboard.Fk_genre " +
                    "INNER JOIN  Style_snowboard ON Style_snowboard.Id_style = Planche_snowboard.Fk_style; ", SQLiteConn);
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                {
                    string marqueSnowboard = SQLiteReader["Marque"].ToString();
                    string genreSnowboard = SQLiteReader["Genre"].ToString();
                    string niveauSnowboard = SQLiteReader["Niveau"].ToString();
                    string styleSnowboard = SQLiteReader["Style"].ToString();
                    string prixSnowboard = SQLiteReader["Prix"].ToString();
                    decimal prixSnowboardDecimal = decimal.Parse(prixSnowboard);
                    snowboardListe.Add(new Snowboard(marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardDecimal)); // ADD Snowboard ITEM IN LIST
                    foreach (var Snowboard in snowboardListe)
                        {
                            Console.WriteLine("Snowboard : {0}, {1}, {2}", Snowboard.Prix, Snowboard.Niveau, Snowboard.Marque);
                        }
                }
                    SQLiteReader.Close(); // FERMETURE READER
                return snowboardListe;
                
            }
        }
        public static void SQLiteAddMarque(string MarqueInsert)
        {
            string connString = "Data Source = C:/Users/adai101/Desktop/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3";
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Marque_snowboard (Marque) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Marque", MarqueInsert);     
                SQLiteInstert.ExecuteNonQuery();                      
            }
        }
        public static void SQLiteAddStyle(string MarqueInsert)
        {
            string connString = "Data Source = C:/Users/adai101/Desktop/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3";
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Style", MarqueInsert);
                SQLiteInstert.ExecuteNonQuery();
            }
        }
    }  
} 
