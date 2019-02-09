﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Projet2_CCI
{
    // CONTIENT REQUETE SQL
    static class SQLHelper
    {
        // QUERY PLANCHE
        public static ObservableCollection<Snowboard> SQLitePlancheRead()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<Snowboard> snowboardListe = new ObservableCollection<Snowboard>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // REQUEST STRING
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Stock,Prix,Niveau,Marque,Genre,Style FROM Planche_snowboard " +
                    "INNER JOIN Niveau_snowboard ON Niveau_snowboard.Id_niveau = Planche_snowboard.Fk_niveau " +
                    "INNER JOIN Marque_snowboard ON Marque_snowboard.Id_marque = Planche_snowboard.Fk_marque " +
                    "INNER JOIN Genre_snowboard ON Genre_snowboard.Id_genre = Planche_snowboard.Fk_genre " +
                    "INNER JOIN  Style_snowboard ON Style_snowboard.Id_style = Planche_snowboard.Fk_style; ", SQLiteConn);

                // OUVERTURE CONNECTION ET LECTURE BD
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
                }
                SQLiteReader.Close(); // FERMETURE READER
            }
            return snowboardListe;
        }
        public static void SQLiteAddMarque(string MarqueInsert)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
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
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Style", MarqueInsert);
                SQLiteInstert.ExecuteNonQuery();
            }
        }
        public static string[] SQLiteConnexion(string username, string password)
        {
            // DEF VARIABLE
            string[] returnValue = new string[3];
            returnValue[0] = "erreur"; // Nom //string.Empty
            returnValue[1] = "erreur"; // prenom // string.Empty
            returnValue[2] = "erreur"; // groupe // string.Empty
            //string valueUsername = "erreur";
            //string valueGroupe;
            //string valueNom;
            //string valuePrenom;


            // DEF SQL
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // SQL QUERY
                string querySelectUser = "SELECT Login,Nom,Prenom,Groupe FROM Employe WHERE Login = @login AND Password = @password LIMIT 1;";
                // SQL QUERY USERNAME
                SQLiteCommand SQLiteCommandUser = new SQLiteCommand(querySelectUser, SQLiteConn);
                SQLiteConn.Open();
                SQLiteCommandUser.Parameters.AddWithValue("login", username);
                SQLiteCommandUser.Parameters.AddWithValue("password", password);

                SQLiteDataReader SQLiteReaderUser = SQLiteCommandUser.ExecuteReader();
                while  (SQLiteReaderUser.Read())
                { 
                    //valueUsername = SQLiteReaderUser["Login"].ToString();
                    returnValue[2] = SQLiteReaderUser["Groupe"].ToString();
                    returnValue[0] = SQLiteReaderUser["Nom"].ToString();
                    returnValue[1] = SQLiteReaderUser["Prenom"].ToString();
                }
                SQLiteReaderUser.Close();
                return returnValue;
            }
              
        }
        public static ObservableCollection<Employe> SQLiteListUsers()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Nom, Prenom, Login, Password, Groupe FROM Employe ", SQLiteConn);
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                { 
                    string nomEmploye = SQLiteReader["Nom"].ToString();
                    string prenomEmploye = SQLiteReader["Prenom"].ToString();
                    string loginEmploye = SQLiteReader["Login"].ToString();
                    string passwordEmploye = SQLiteReader["Password"].ToString();
                    string groupeEmploye = SQLiteReader["Groupe"].ToString();
                    usersList.Add(new Employe(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye));
                }
                SQLiteReader.Close();
                
            }
            return usersList;
        }
    }
} 