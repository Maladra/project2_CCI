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
        public static List<string> SqlReadMarque()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Marque FROM Marque_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<string> listeMarque = new List<string>();
                while (sqliteReader.Read())
                {
                    string marque = sqliteReader["Marque"].ToString();
                    listeMarque.Add(marque);
                }
                return listeMarque;

            }

        }



    }

}
