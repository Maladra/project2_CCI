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
