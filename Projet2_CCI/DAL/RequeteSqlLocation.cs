using Projet2_CCI.Donnee;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Projet2_CCI.DAL
{
    public class RequeteSqlLocation
    {
        public static bool insertLocationSnowboard(LocationAvecListeSnowboard location)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();


                // VERIFICATION PRESENCE CLIENT
                string selectClient = "SELECT Id_client,Nom, Prenom" +
                    "FROM Client " +
                    "WHERE Nom=@nom AND Prenom=@prenom LIMIT 1;";

                //INSERT INTO Employe(Nom, Prenom, Login, Password, Groupe, Salt) VALUES(?,?,?,?,?,?)

                SQLiteCommand SQLiteVerificationClient = new SQLiteCommand(selectClient, SqliteConnection);
                bool verificationExist = true;
                SQLiteVerificationClient.Parameters.AddWithValue("@nom", location.ClientLocation.Nom);
                SQLiteVerificationClient.Parameters.AddWithValue("@prenom", location.ClientLocation.Prenom);
                using (SQLiteDataReader SQLiteReadClient = SQLiteVerificationClient.ExecuteReader())
                {
                    if (!SQLiteReadClient.Read())
                    {
                        string insertClient = "INSERT INTO Client(Nom, Prenom, Numero_telephone) VALUES(?,?,?)";

                        using (SQLiteCommand SQLiteInsert = new SQLiteCommand(insertClient, SqliteConnection))
                        {
                            SQLiteInsert.Parameters.AddWithValue("@Nom", location.ClientLocation.Nom);
                            SQLiteInsert.Parameters.AddWithValue("@Prenom", location.ClientLocation.Prenom);
                            SQLiteInsert.Parameters.AddWithValue("@Numero_telephone", location.ClientLocation.NumeroTelephone);
                            SQLiteInsert.ExecuteNonQuery();
                            verificationExist = false;
                        }
                    }
                    else
                    {

                    }
                }



                string creationLocation = "INSERT INTO Location (Moyen_paiement, Date_debut," +
                    " Date_fin, Tva, En_cours, Fk_Client)" +
                    "VALUES (?,?,?,?,?,?)";
                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationLocation, SqliteConnection))
                {
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Moyen_paiement", location.MoyenPaiement);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Date_debut", location.DateDebut);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Date_fin", location.DateFin);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Tva", location.Tva);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@En_cours", location.Etat);
                    SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_Client", SqliteConnection.LastInsertRowId);
                    SqlInsertPlancheLouee.ExecuteNonQuery();
                }

                string creationSnowboardLocation = "INSERT INTO Planche_louee (Prix_location_euro," +
                    " Prix_location_dollar, Fk_planche, Fk_location) VALUES (?,?,?,?)";
                using (SQLiteCommand SqlInsertPlancheLouee = new SQLiteCommand(creationSnowboardLocation,
                    SqliteConnection))
                {
                    foreach (var snowboard in location.ListeSnowboard)
                    {
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_euro", snowboard.PrixEuro);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Prix_location_dollar", snowboard.PrixDollar);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_planche", snowboard.IdSnowboard);
                        SqlInsertPlancheLouee.Parameters.AddWithValue("@Fk_location", lastIdInsert);
                    }
                }
                return true;
            }
        }

        public List<Location> listLocationSnowboard()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            List<Location> listeLocation = new List<Location>();

            using (SQLiteConnection SqliteConnection = new SQLiteConnection(connString))
            {
                SqliteConnection.Open();

                string SelectLocation = "SELECT Moyen_paiement, Date_debut, Date_fin, Tva, En_cours, Nom, Prenom, Numero_telephone" +
                    " FROM LOCATION WHERE Location.Fk_Client = @idClient";
                using (SQLiteCommand SqlCommandSelect = new SQLiteCommand(SelectLocation, SqliteConnection))
                {
                    using (SQLiteDataReader SqliteReader = SqlCommandSelect.ExecuteReader())
                    {
                        while (SqliteReader.Read())
                        {
                            //TODO : A MODIFIER 
                            Client clientLocation = new Client((string)SqliteReader["Nom"], (string)SqliteReader["Prenom"],
                                (string)SqliteReader["Numero_telephone"]);

                            string prenomClientLocation = (string)SqliteReader["Prenom"];
                            string moyenPaiementClient = (string)SqliteReader["Moyen_paiement"];
                            DateTime dateDebutLocation = (DateTime)SqliteReader["Date_debut"];
                            DateTime dateFinLocation = (DateTime)SqliteReader["Date_fin"];
                            decimal tva = (decimal)SqliteReader["Tva"] * 100;
                            string etatLocation = (string)SqliteReader["En_cours"];
                            listeLocation.Add(new Location(clientLocation, moyenPaiementClient,
                                dateDebutLocation, dateFinLocation, tva, etatLocation));
                        }
                    }
                    return listeLocation;
                }

                //using ()
                //{
                //
                //}












            }
        }
    }
}