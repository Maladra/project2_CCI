﻿using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace Projet2_CCI.DAL
{
    class RequeteSqlStyle
    {
        /// <summary>
        /// Prend un string et fait une requete SQL pour inserer un style de snowboard dans la BD
        /// </summary>
        public static bool SQLiteAddStyle(string style)
        {
            // TODO : VERIFIER QUE LE STYLE N'EST PAS DEJA PRESENT
            string connString = ConfigurationManager.AppSettings["connectionString"];

            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                string querySelect = "SELECT Style FROM Style_snowboard WHERE Style =@style";
                sqliteConn.Open();
                using (SQLiteCommand sqliteRead = new SQLiteCommand(querySelect, sqliteConn))
                {
                    sqliteRead.Parameters.AddWithValue("@style", style);
                    using (SQLiteDataReader sqliteReadStyle = sqliteRead.ExecuteReader())
                    {
                        if (sqliteReadStyle.Read())
                        {
                            return false;
                        }
                    }

                }
                using (SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, sqliteConn))
                {
                    SQLiteInsert.Parameters.AddWithValue("@Style", style);
                    SQLiteInsert.ExecuteNonQuery();
                    return true;
                }
                
            }
        }

        public static List<string> SqlReadStyle()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Style FROM Style_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<string> listeStyle = new List<string>();
                while (sqliteReader.Read())
                {
                    string style = sqliteReader["Style"].ToString();
                    listeStyle.Add(style);
                }
                return listeStyle;
                
            }
                
        }
    }
}
