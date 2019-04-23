using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Projet2_CCI.DAL
{
    public class TestDb
    {

        public static string ServerConnected()
        {

            string connString = ConfigurationManager.AppSettings["connectionString"];



            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                try
                {
                    sqliteConn.Open();
                    return "Connexion établie";
                }
                catch (Exception e)
                {
                    return e.Message;

                }
            }
        }
    }
}
