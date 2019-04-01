using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class RequeteSqlGenre
    {
        public static List<string> SqlReadGenre()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Genre FROM Genre_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<string> listeGenre = new List<string>();
                while (sqliteReader.Read())
                {
                    string genre = sqliteReader["Genre"].ToString();
                    listeGenre.Add(genre);
                }
                return listeGenre;

            }

        }

    }
}
