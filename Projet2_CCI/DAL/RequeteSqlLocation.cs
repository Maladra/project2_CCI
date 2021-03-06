﻿using Models;
using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using static Projet2_CCI.LocationAvecId;

namespace Projet2_CCI.DAL
{
    public class RequeteSqlLocation
    {

        /// <summary>
        /// Prend un objet LocationAvecListeSnowboard comme paramètre, vérifie la présence du client sinon l'ajoute, 
        /// et insert dans la DB la location avec les planches associées
        /// </summary>
        public static bool insertLocationSnowboard(LocationAvecListeSnowboard location)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();


                long lastId = default(long);

                string selectClient = "SELECT *" +
                    "FROM Client " +
                    "WHERE Nom=@nom AND Prenom=@prenom LIMIT 1;";

                using (SQLiteCommand SQLiteVerificationClient = new SQLiteCommand(selectClient, SqliteConnection))
                {
                    SQLiteVerificationClient.Parameters.AddWithValue("@Nom", location.ClientLocation.Nom);
                    SQLiteVerificationClient.Parameters.AddWithValue("@Prenom", location.ClientLocation.Prenom);

                    using (SQLiteDataReader SQLiteReadClient = SQLiteVerificationClient.ExecuteReader())
                    {
                        if (!SQLiteReadClient.Read())
                        {
                            string insertClient = "INSERT INTO Client(Nom, Prenom, Numero_telephone) VALUES(?,?,?)";

                            using (SQLiteCommand SQLiteInsert = new SQLiteCommand(insertClient, SqliteConnection))
                            {
                                SQLiteInsert.Parameters.AddWithValue("@Nom", location.ClientLocation.Nom);
                                SQLiteInsert.Parameters.AddWithValue("@Prenom", location.ClientLocation.Prenom);
                                SQLiteInsert.Parameters.AddWithValue("@Numero_telephone", location.ClientLocation.NumeroTelephone);
                                SQLiteInsert.ExecuteNonQuery();
                                lastId = SqliteConnection.LastInsertRowId;
                            }
                        }
                        else
                        {
                            lastId = (long)SQLiteReadClient["Id_client"];
                        }
                    }

                }

                string creationLocation = "INSERT INTO Location (Moyen_paiement, Date_debut," +
                    " Date_fin, Tva, En_cours, Fk_Client)" +
                    "VALUES (?,?,?,?,?,?); SELECT last_insert_rowid()";
                ;
                using (SQLiteCommand SqlInsertLocation = new SQLiteCommand(creationLocation, SqliteConnection))
                {
                    SqlInsertLocation.Parameters.AddWithValue("@Moyen_paiement", location.MoyenPaiement);
                    SqlInsertLocation.Parameters.AddWithValue("@Date_debut", location.DateDebut);
                    SqlInsertLocation.Parameters.AddWithValue("@Date_fin", location.DateFin);
                    SqlInsertLocation.Parameters.AddWithValue("@Tva", location.Tva);
                    SqlInsertLocation.Parameters.AddWithValue("@En_cours", location.Etat);
                    SqlInsertLocation.Parameters.AddWithValue("@Fk_Client", lastId);
                    lastId = (long)SqlInsertLocation.ExecuteScalar();

                }

                string creationSnowboardLocation = "INSERT INTO Planche_louee (Prix_location_euro," +
                    " Prix_location_dollar, Fk_planche, Fk_location, Quantite) VALUES (?,?,?,?,?)";

                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationSnowboardLocation,
                    SqliteConnection))
                {
                    foreach (var snowboard in location.ListeSnowboard)
                    {
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_euro", snowboard.PrixEuro);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_dollar", snowboard.PrixDollar);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_planche", snowboard.IdSnowboard);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_location", lastId);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Quantite", snowboard.Stock);
                        SqlInsertPlancheLouee.ExecuteNonQuery();
                    }
                }
                return true;
            }
        }


        /// <summary>
        /// Prend un int (représente la quantité) et un long (représente l'Id) et remplace la valeur dans la DB
        /// </summary>
        public static void updateStockSnowboard(int snowboardStock, long snowboardId)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];

            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();
                string updateStock = @"UPDATE Planche_snowboard
                SET Stock = @stock
                WHERE Id_planche = @idPlanche";
                using (SQLiteCommand SqliteUpdateSnowboardCommand = new SQLiteCommand(updateStock, SqliteConnection))
                {
                    SqliteUpdateSnowboardCommand.Parameters.AddWithValue("@stock", snowboardStock);
                    SqliteUpdateSnowboardCommand.Parameters.AddWithValue("@idPlanche", snowboardId);
                    SqliteUpdateSnowboardCommand.ExecuteNonQuery();

                }
            }

        }


        /// <summary>
        /// Prend un long (représente l'Id client) et retourne une liste de location
        /// </summary>
        public static List<DynamicLocationId> listLocationSnowboard(long idClient)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            List<DynamicLocationId> listeLocation = new List<DynamicLocationId>();

            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();

                string SelectLocation = "SELECT Id_location,Moyen_paiement, Date_debut, Date_fin, Tva, En_cours" +
                    " FROM Location WHERE Location.Fk_Client = @idClient";
                using (SQLiteCommand SqlCommandSelect = new SQLiteCommand(SelectLocation, SqliteConnection))
                {
                    SqlCommandSelect.Parameters.AddWithValue("@idClient", idClient);
                    using (SQLiteDataReader SqliteReader = SqlCommandSelect.ExecuteReader())
                    {
                        while (SqliteReader.Read())
                        {
                            //TODO : A MODIFIER 
                            long idLocation = (long)SqliteReader["Id_location"];
                            string moyenPaiementClient = (string)SqliteReader["Moyen_paiement"];
                            DateTime dateDebutLocation = (DateTime)SqliteReader["Date_debut"];
                            DateTime dateFinLocation = (DateTime)SqliteReader["Date_fin"];
                            decimal tva = (decimal)SqliteReader["Tva"] * 100;
                            string etatLocation = (string)SqliteReader["En_cours"];
                            listeLocation.Add(new DynamicLocationId(idLocation, moyenPaiementClient,
                                dateDebutLocation, dateFinLocation, tva, etatLocation));
                        }
                    }
                    return listeLocation;
                }

                //using ()
                //{
                //
                //}












            }
        }

        /// <summary>
        /// Prend un long represente l'Id de la location et un string represente l'etat et met à jour la DB
        /// </summary>
        public static bool updateEtatLocation(long idLocation, string etat)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                string updateLocation = "UPDATE Location SET En_cours = @etat WHERE Id_location=@idLocation ";

                SqliteConnection.Open();
                using (SQLiteCommand SqliteUpdateEtat = new SQLiteCommand(updateLocation, SqliteConnection))
                {
                    SqliteUpdateEtat.Parameters.AddWithValue("@idLocation", idLocation);
                    SqliteUpdateEtat.Parameters.AddWithValue("@etat", etat);
                    SqliteUpdateEtat.ExecuteNonQuery();
                }

                return true;
            }
        }

        /// <summary>
        /// Prend un Long représente l'Id d'une location et retourne une liste de planche assignée a la location
        /// </summary>
        public static List<PlancheLouee> SelectPlancheLouee(long idLocation)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                string selectPlancheLouee = "SELECT Fk_planche, Quantite FROM Planche_louee WHERE Fk_location=@idLocation";
                List<PlancheLouee> listePlancheLouee = new List<PlancheLouee>();
                SqliteConnection.Open();

                using (SQLiteCommand SqliteSelectPlancheLouee = new SQLiteCommand(selectPlancheLouee, SqliteConnection))
                {
                    SqliteSelectPlancheLouee.Parameters.AddWithValue("@idLocation", idLocation);
                    SQLiteDataReader SqliteReader = SqliteSelectPlancheLouee.ExecuteReader();


                    while (SqliteReader.Read())
                    {

                        long idPlanche = (long)SqliteReader["Fk_planche"];
                        int quantite = (int)(long)SqliteReader["Quantite"];
                        listePlancheLouee.Add(new PlancheLouee(idPlanche, quantite));
                    }
                }
                return listePlancheLouee;

            }
        }

        /// <summary>
        /// Retourne une planche de snowboard du stock ou l'Id correspond a sa FK dans Planche_Louee
        /// </summary>
        public static PlancheLouee SelectPlancheStock(PlancheLouee plancheLouee)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();
                string selectPlanche = "SELECT Id_planche, Stock FROM Planche_snowboard WHERE Id_planche=@idPlancheLocation";

                using (SQLiteCommand SqliteSelectPlanche = new SQLiteCommand(selectPlanche, SqliteConnection))
                {
                    SqliteSelectPlanche.Parameters.AddWithValue("@idPlancheLocation", plancheLouee.IdPlanche);
                    using (SQLiteDataReader SqliteReader = SqliteSelectPlanche.ExecuteReader())
                    {
                        if (SqliteReader.Read())
                        {
                            long idPlanche = (long)SqliteReader["Id_planche"];
                            int stock = (int)(long)SqliteReader["Stock"];
                            PlancheLouee plancheStock = new PlancheLouee(idPlanche, stock);
                            return plancheStock;
                        }

                    }
                    return null;
                }

            }
        }

        /// <summary>
        /// Prend un int (représente la quantité) et un long (représente l'Id) et remplace la valeur dans la DB
        /// </summary>
        public static void UpdateStock(long idPlanche, int quantite)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();
                string updateStock = "UPDATE Planche_snowboard SET Stock = @quantite WHERE Id_planche = @idPlanche ";
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand(updateStock, SqliteConnection))
                {
                    sQLiteCommand.Parameters.AddWithValue("@quantite", quantite);
                    sQLiteCommand.Parameters.AddWithValue("@idPlanche", idPlanche);
                    sQLiteCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
