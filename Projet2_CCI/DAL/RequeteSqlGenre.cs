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
    public class RequeteSqlGenre
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
                    var id = (int)(long)sqliteReader["Id_genre"];
                    string nomGenre = sqliteReader["Genre"].ToString();
                    Genre genre = new Genre(id, nomGenre);
                    listeGenre.Add(genre);
                }
                return listeGenre;

            }

        }

    }
}
