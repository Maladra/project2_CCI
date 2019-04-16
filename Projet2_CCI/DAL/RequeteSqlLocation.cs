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
            return true;
        }

       /* public bool LocationSnowboard(Location location)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                string creationSnowboard = "INSERT INTO Planche_louee (Prix_location_euro," +
                    " Prix_location_dollar, Fk_planche, Fk_location) VALUES (?,?,?,?)";
                SqliteConnection.Open();
                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationSnowboard, SqliteConnection)
                {
                    ;
                }

            }
            return true;
        } */
    }
}