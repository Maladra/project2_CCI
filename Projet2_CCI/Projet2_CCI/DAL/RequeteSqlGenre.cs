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
    class RequeteSqlGenre
    {
        public static List<Genre> SqlReadGenre()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Id_genre,Genre FROM Genre_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<Genre> listeGenre = new List<Genre>();
                while (sqliteReader.Read())
                {
                    string idGenre = sqliteReader["Id_genre"].ToString();
                    string nomGenre = sqliteReader["Genre"].ToString();
                    Genre genre = new Genre(idGenre, nomGenre);
                    listeGenre.Add(genre);
                }
                return listeGenre;

            }

        }

    }
}
