using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace Projet2_CCI.DAL
{
    class RequeteSqlStyle
    {
        /// <summary>
        /// Prend un string et fait une requete SQL pour inserer un style de snowboard dans la BD
        /// </summary>
        public static void SQLiteAddStyle(string style)
        {
            // TODO : VERIFIER QUE LE STYLE N'EST PAS DEJA PRESENT
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                sqliteConn.Open();
                SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, sqliteConn);
                SQLiteInsert.Parameters.AddWithValue("@Style", style);
                SQLiteInsert.ExecuteNonQuery();
            }
        }

        public static List<string> sqlReadStyle()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Style FROm Style_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<string> listeMarque = new List<string>();
                while (sqliteReader.Read())
                {
                    string Style = sqliteReader["Style"].ToString();
                    listeMarque.Add(Style);
                }
                return listeMarque;
                
            }
                
        }
    }
}
