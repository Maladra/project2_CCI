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
        public static List<string> SqlReadNiveau()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string querySelect = "SELECT Niveau FROM Niveau_snowboard ";
                sqliteConn.Open();
                SQLiteCommand sqliteSelect = new SQLiteCommand(querySelect, sqliteConn);
                SQLiteDataReader sqliteReader = sqliteSelect.ExecuteReader();
                List<string> listeNiveau = new List<string>();
                while (sqliteReader.Read())
                {
                    string niveau = sqliteReader["Niveau"].ToString();
                    listeNiveau.Add(niveau);
                }
                return listeNiveau;

            }

        }
    }
}
