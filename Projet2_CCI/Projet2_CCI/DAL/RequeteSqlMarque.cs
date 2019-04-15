using Projet2_CCI.Donnee;
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
        public static bool SQLiteAddMarque(string marque)
        {

            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Marque FROM Marque_snowboard WHERE Marque = @marque";
                string queryInsert = "INSERT INTO Marque_snowboard (Marque) VALUES (?)";
                SQLiteConn.Open();
                using (SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, SQLiteConn)) 
                {
                    sqliteSelect.Parameters.AddWithValue("@marque", marque);
                    using (SQLiteDataReader sqliteSelectMarque = sqliteSelect.ExecuteReader())
                    {
                        if (sqliteSelectMarque.Read())
                        {
                            return false;
                        }
                    }

                }
    
                using (SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn))
                {
                    SQLiteInsert.Parameters.AddWithValue("@Marque", marque);
                    SQLiteInsert.ExecuteNonQuery();
                    return true;
                }
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
                    int idMarque = (int)(long)sqliteReader["id_marque"];
                    string nomMarque = sqliteReader["Marque"].ToString();
                    Marque marque = new Marque(idMarque, nomMarque);
                    listeMarque.Add(marque);
                }
                return listeMarque;

            }

        }



    }

}
