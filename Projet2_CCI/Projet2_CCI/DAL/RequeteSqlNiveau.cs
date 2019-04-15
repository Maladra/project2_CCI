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
    class RequeteSqlNiveau
    {
        public static List<Niveau> SqlReadNiveau()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Id_niveau ,Niveau FROM Niveau_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<Niveau> listeNiveau = new List<Niveau>();
                while (sqliteReader.Read())
                {
                    string Id = sqliteReader["Id_niveau"].ToString();
                    string nomNiveau = sqliteReader["Niveau"].ToString();
                    Niveau niveau = new Niveau(Id, nomNiveau);
                    listeNiveau.Add(niveau);
                }
                return listeNiveau;

            }

        }
    }
}
