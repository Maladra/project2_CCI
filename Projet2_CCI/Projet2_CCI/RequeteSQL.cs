using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI
{
    // CONTIENT REQUETE SQL
    static class SQLHelper
    {
        // QUERY PLANCHE
        public static ObservableCollection<Snowboard> SQLitePlancheRead()
        {
            string connString = "Data Source = D:/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3"; // CONNECTION STRING
            ObservableCollection<Snowboard> snowboardListe = new ObservableCollection<Snowboard>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT * FROM Planche_snowboard;", SQLiteConn);
                    SQLiteCommand.Connection.Open();
                    SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                    while (SQLiteReader.Read())
                    {
                        string marqueSnowboard = SQLiteReader["Fk_marque"].ToString();
                        string genreSnowboard = SQLiteReader["Fk_genre"].ToString();
                        string niveauSnowboard = SQLiteReader["Fk_niveau"].ToString();
                        string styleSnowboard = SQLiteReader["Fk_style"].ToString();
                        string prixSnowboard = SQLiteReader["Prix"].ToString();
                        decimal prixSnowboardDecimal = decimal.Parse(prixSnowboard);

                        snowboardListe.Add(new Snowboard(marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardDecimal)); // ADD Snowboard ITEM IN LIST
                        foreach (var Snowboard in snowboardListe)
                        {
                            Console.WriteLine("Snowboard : {0}, {1}, {2}", Snowboard.Prix, Snowboard.Niveau, Snowboard.Marque);
                        }
                    }
                    SQLiteReader.Close(); // FERMETURE CONNEXION
                    SQLiteConn.Close();
                }
                catch
                {

                }
                return snowboardListe;
                
            };
        }
        public static void SQLiteAddMarque(string MarqueInsert)
        {
            string connString = "Data Source = D:/project2_CCI/Projet2_CCI/dataBase/gestion.db; Version = 3";
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Marque_snowboard (Marque) VALUES (?)";
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Marque", MarqueInsert);

                try {
                    SQLiteInstert.ExecuteNonQuery();
                                       
                }
                catch
                {

                }
                SQLiteConn.Close();

            }
                    }
    }
} 
