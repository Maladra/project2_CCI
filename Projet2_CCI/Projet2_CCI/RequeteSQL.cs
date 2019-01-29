using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    static class SQLHelper
    {
        public static void SQLiteRead(string requeteString, string stringConnection)
        {

            using (SQLiteConnection SQLiteConn = new SQLiteConnection(stringConnection))
            {
                try
                {
                    SQLiteCommand SQLiteCommand = new SQLiteCommand(requeteString, SQLiteConn);
                    SQLiteCommand.Connection.Open();
                    SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                    while (SQLiteReader.Read())
                    {
                        Console.WriteLine("{0}--{1}", SQLiteReader[0], SQLiteReader[1]);
                    }
                    SQLiteReader.Close();
                    SQLiteConn.Close();
                }
                catch
                {

                }
            }
        }
    }
}
        //Data Source = DataBase/gestion.db;Version=3"
 
