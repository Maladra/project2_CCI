using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Projet2_CCI.DAL
{
    public class RequeteSqlLocation
    {

        public bool SqlListLocation()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
                return true;
        }

        public bool LocationSnowboard(Location location)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                long lastIdInsert = default(long);
                SqliteConnection.Open();

                string creationLocation = "INSERT INTO Location (Moyen_paiement, Date_debut," +
                    " Date_fin, Tva, En_cours" +
                    "VALUES (?,?,?,?,?)";
                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationLocation, SqliteConnection))
                {
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Moyen_paiement", location.MoyenPaiement);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Date_debut", location.DateDebut);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Date_fin", location.DateFin);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Tva", location.Tva);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@En_cours", location.Etat);
                    SqlInsertPlancheLouee.ExecuteNonQuery();
                    lastIdInsert = SqliteConnection.LastInsertRowId;
                }

                string creationSnowboardLocation = "INSERT INTO Planche_louee (Prix_location_euro," +
                    " Prix_location_dollar, Fk_planche, Fk_location) VALUES (?,?,?,?)";
                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationSnowboardLocation,
                    SqliteConnection))
                {
                    foreach (var snowboard in location.ListeSnowboard)
                    {
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_euro", snowboard.PrixEuro);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_dollar", snowboard.PrixDollar);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_planche", snowboard.IdSnowboard);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_location", lastIdInsert);
                    }  
                }
                return true;
            }
        }
    }
}