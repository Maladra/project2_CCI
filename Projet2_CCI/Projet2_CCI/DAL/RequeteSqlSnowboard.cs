using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class RequeteSqlSnowboard
    {
        /// <summary>
        /// Requete SQL qui retourne une liste de planche de snowboard
        /// </summary>
        public static ObservableCollection<DAL.SnowboardRequete> SQLitePlancheRead()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<SnowboardRequete> snowboardListe = new ObservableCollection<SnowboardRequete>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // REQUEST STRING
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Nom_modele,Stock,Prix_euro,Prix_dollar,Niveau,Marque,Genre,Style FROM Planche_snowboard " +
                    "INNER JOIN Niveau_snowboard ON Niveau_snowboard.Id_niveau = Planche_snowboard.Fk_niveau " +
                    "INNER JOIN Marque_snowboard ON Marque_snowboard.Id_marque = Planche_snowboard.Fk_marque " +
                    "INNER JOIN Genre_snowboard ON Genre_snowboard.Id_genre = Planche_snowboard.Fk_genre " +
                    "INNER JOIN Style_snowboard ON Style_snowboard.Id_style = Planche_snowboard.Fk_style;", SQLiteConn);

                // OUVERTURE CONNECTION ET LECTURE BD
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                {
                    string nomSnowboard = SQLiteReader["Nom_modele"].ToString();
                    string marqueSnowboard = SQLiteReader["Marque"].ToString();
                    string genreSnowboard = SQLiteReader["Genre"].ToString();
                    string niveauSnowboard = SQLiteReader["Niveau"].ToString();
                    string styleSnowboard = SQLiteReader["Style"].ToString();
                    string prixSnowboarEuro = SQLiteReader["Prix_euro"].ToString();
                    string prixSnowboarDollar = SQLiteReader["Prix_euro"].ToString();
                    int prixSnowboardEuroDecimal = int.Parse(prixSnowboarEuro);
                    int prixSnowboardDollarDecimal = int.Parse(prixSnowboarDollar);
                    string stockSnowboard = SQLiteReader["Stock"].ToString();
                    snowboardListe.Add(new SnowboardRequete(nomSnowboard, marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardEuroDecimal, prixSnowboardDollarDecimal, Convert.ToInt32(stockSnowboard))); // ADD Snowboard to LIST
                }
                SQLiteReader.Close(); // FERMETURE READER
            }
            return snowboardListe;
        } 

        public static void SQLAddSnowboard (SnowboardRequete snowboard)
        {
            //TODO: tronquer chiffre (2 chiffre après virgule et comparer les 2)
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteCon = new SQLiteConnection(connString))
            {
                // VOIR LA DOC ENVOYEE SUR MON DISCORD
                SQLiteCommand sqliteCommand = new SQLiteCommand("INSERT INTO Planche_snowboard(Stock, Prix_euro, Prix_dollar, Nom_modele, Fk_niveau, Fk_marque, Fk_genre, Fk_style) VALUES (?,?,?,?,?,?,?,?)");

            }
            
        }
    }
}
