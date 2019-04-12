using Projet2_CCI.Donnee;
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

        public static ObservableCollection<Donnee.SnowboardRequeteId> SQLitePlancheRead()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<SnowboardRequeteId> snowboardListe = new ObservableCollection<SnowboardRequeteId>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // REQUEST STRING
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Id_planche,Nom_modele,Stock,Prix_euro,Prix_dollar,Niveau,Marque,Genre,Style FROM Planche_snowboard " +
                    "INNER JOIN Niveau_snowboard ON Niveau_snowboard.Id_niveau = Planche_snowboard.Fk_niveau " +
                    "INNER JOIN Marque_snowboard ON Marque_snowboard.Id_marque = Planche_snowboard.Fk_marque " +
                    "INNER JOIN Genre_snowboard ON Genre_snowboard.Id_genre = Planche_snowboard.Fk_genre " +
                    "INNER JOIN Style_snowboard ON Style_snowboard.Id_style = Planche_snowboard.Fk_style;", SQLiteConn);
        
                // OUVERTURE CONNECTION ET LECTURE BD
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                {
                    long idSnowboard = (long)SQLiteReader["Id_planche"];
                    string nomSnowboard = SQLiteReader["Nom_modele"].ToString();
                    string marqueSnowboard = SQLiteReader["Marque"].ToString();
                    string genreSnowboard = SQLiteReader["Genre"].ToString();
                    string niveauSnowboard = SQLiteReader["Niveau"].ToString();
                    string styleSnowboard = SQLiteReader["Style"].ToString();
                    decimal prixSnowboarEuro = (decimal)SQLiteReader["Prix_euro"];
                    decimal prixSnowboarDollar = (decimal)SQLiteReader["Prix_dollar"];
                    decimal prixSnowboardEuroDecimal = prixSnowboarEuro/100;
                    decimal prixSnowboardDollarDecimal = prixSnowboarDollar/100;
                    long stockSnowboard = (long)SQLiteReader["Stock"];
                    snowboardListe.Add(new SnowboardRequeteId(idSnowboard,nomSnowboard, marqueSnowboard, 
                        genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardEuroDecimal, 
                        prixSnowboardDollarDecimal, Convert.ToInt32(stockSnowboard))); // ADD Snowboard to LIST
                }
                SQLiteReader.Close(); // FERMETURE READER
            }
            return snowboardListe;
        } 

        public static bool SQLAddSnowboard (SnowboardRequete snowboard)
        {
            // TODO: VERIFICATION PRESENCE PLANCHE (SI LE TEMPS)
            //string queryVerificationSnowboard = "SELECT * FROM Planche_snowboard WHERE Login = @login";
            string queryInsertSnowboard = "INSERT INTO " +
                    "Planche_snowboard(Stock, Prix_euro, Prix_dollar, Nom_modele, Fk_niveau," +
                    " Fk_marque, Fk_genre, Fk_style) VALUES (?,?,?,?,?,?,?,?)";

            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection sqliteCon = new SQLiteConnection(connString))
            {

                using (SQLiteCommand sqliteCommand = new SQLiteCommand(queryInsertSnowboard, sqliteCon))
                {
                    sqliteCommand.Connection.Open();
                    sqliteCommand.Parameters.AddWithValue("@Stock", snowboard.Stock);
                    sqliteCommand.Parameters.AddWithValue("@Prix_euro", snowboard.PrixEuro);
                    sqliteCommand.Parameters.AddWithValue("@Prix_dollar", snowboard.PrixDollar);
                    sqliteCommand.Parameters.AddWithValue("@Nom_modele", snowboard.Nom);
                    sqliteCommand.Parameters.AddWithValue("@Fk_niveau", snowboard.Niveau);
                    sqliteCommand.Parameters.AddWithValue("@Fk_marque", snowboard.Marque);
                    sqliteCommand.Parameters.AddWithValue("@Fk_genre", snowboard.Genre);
                    sqliteCommand.Parameters.AddWithValue("@Fk_style", snowboard.Style);
                    sqliteCommand.ExecuteNonQuery();
                    return true;

                };
                //return false;
            }
            
        }
    }
}
