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
    static class SQLHelper
    {
        public static ObservableCollection SQLiteRead(string requeteString, string stringConnection)
        {

            using (SQLiteConnection SQLiteConn = new SQLiteConnection(stringConnection))
            {
                ObservableCollection<Snowboard> snowboardListe = new ObservableCollection<Snowboard>();
                try
                {
                    SQLiteCommand SQLiteCommand = new SQLiteCommand(requeteString, SQLiteConn);
                    SQLiteCommand.Connection.Open();
                    SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                    while (SQLiteReader.Read())
                    {
                        string marqueSnowboard = SQLiteReader["Fk_marque"].ToString();
                        string genreSnowboard = SQLiteReader["Fk_genre"].ToString();
                        string niveauSnowboard = SQLiteReader["Fk_niveau"].ToString();
                        string styleSnowboard = SQLiteReader["Fk_style"].ToString();
                        string prixSnowboard = SQLiteReader["Prix"].ToString();
                        decimal test = decimal.Parse(prixSnowboard);
                        //Snowboard snowboard = new Snowboard(marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, test);
                        //Console.WriteLine(SQLiteReader[0]);
                        //Console.WriteLine(SQLiteReader[2]);

                        //Console.WriteLine(snowboard);
                        //Console.WriteLine(genreSnowboard);
                        snowboardListe.Add(new Snowboard(marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, test));
                        //snowboardListe.Add(snowboard1);
                        foreach (var Snowboard in snowboardListe)
                        {
                            Console.WriteLine("Snowboard : {0}, {1}, {2}", Snowboard.Prix, Snowboard.Niveau, Snowboard.Marque);

                        }

                        //Console.WriteLine(snowboardListe[1]);
                        //Console.WriteLine(snowboardListe[1]);
                        //Console.WriteLine(snowboard);
                    }
                    SQLiteReader.Close();
                    SQLiteConn.Close();
                    //return snowboardListe
                }
                catch
                {

                }
                return snowboardListe;
            }
        }
    }
} 
