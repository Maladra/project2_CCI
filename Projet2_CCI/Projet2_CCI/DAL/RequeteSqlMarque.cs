﻿using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class RequeteSqlMarque
    {
        /// <summary>
        /// Prend un string et fait une requete SQL pour inserer une marque de snowboard dans la BD
        /// </summary>
        public static void SQLiteAddMarque(string marque)
        {

            // TODO : VERIFIER QUE LA MARQUE N'EST PAS DEJA PRESENT
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Marque_snowboard (Marque) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInsert.Parameters.AddWithValue("@Marque", marque);
                SQLiteInsert.ExecuteNonQuery();
            }
        }

        public static List<Marque> SqlReadMarque()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Id_marque ,Marque FROM Marque_snowboard ORDER BY Marque;";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<Marque> listeMarque = new List<Marque>();
                while (sqliteReader.Read())
                {
                    string idMarque = sqliteReader["id_marque"].ToString();
                    string nomMarque = sqliteReader["Marque"].ToString();
                    Marque marque = new Marque(idMarque, nomMarque);
                    listeMarque.Add(marque);
                }
                return listeMarque;

            }

        }



    }

}
