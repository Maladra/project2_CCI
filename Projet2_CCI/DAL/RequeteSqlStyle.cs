using Projet2_CCI.Donnee;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace Projet2_CCI.DAL
{
    public class RequeteSqlStyle
    {
        /// <summary>
        /// Prend un string et fait une requete SQL pour vérifier la présence dans la DB
        /// et inserer le style de snowboard dans la DB si non présent
        /// </summary>
        public static bool SQLiteAddStyle(string style)
        {
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

        /// <summary>
        /// Retourne une liste de style
        /// </summary>
        public static List<Style> SqlReadStyle()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Id_style, Style FROM Style_snowboard ORDER BY Style;";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<Style> listeStyle = new List<Style>();
                while (sqliteReader.Read())
                {
                    int Id = (int)(long)sqliteReader["Id_style"];
                    string styleNom = sqliteReader["Style"].ToString();
                    Style style = new Style(Id, styleNom);
                    listeStyle.Add(style);
                }
                return listeStyle;
                
            }
                
        }
    }
}
