using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Projet2_CCI.DAL;

namespace Projet2_CCI
{
    // CONTIENT REQUETE SQL
    static class SQLHelper
    {
        // QUERY PLANCHE
        public static ObservableCollection<DAL.SnowboardRequete> SQLitePlancheRead()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<SnowboardRequete> snowboardListe = new ObservableCollection<SnowboardRequete>(); // RETURNED VALUE
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // REQUEST STRING
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Nom,Stock,Prix,Niveau,Marque,Genre,Style FROM Planche_snowboard " +
                    "INNER JOIN Niveau_snowboard ON Niveau_snowboard.Id_niveau = Planche_snowboard.Fk_niveau " +
                    "INNER JOIN Marque_snowboard ON Marque_snowboard.Id_marque = Planche_snowboard.Fk_marque " +
                    "INNER JOIN Genre_snowboard ON Genre_snowboard.Id_genre = Planche_snowboard.Fk_genre " +
                    "INNER JOIN  Style_snowboard ON Style_snowboard.Id_style = Planche_snowboard.Fk_style; ", SQLiteConn);

                // OUVERTURE CONNECTION ET LECTURE BD
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                {
                    string nomSnowboard = SQLiteReader["Nom"].ToString();
                    string marqueSnowboard = SQLiteReader["Marque"].ToString();
                    string genreSnowboard = SQLiteReader["Genre"].ToString();
                    string niveauSnowboard = SQLiteReader["Niveau"].ToString();
                    string styleSnowboard = SQLiteReader["Style"].ToString();
                    string prixSnowboard = SQLiteReader["Prix"].ToString();
                    decimal prixSnowboardDecimal = decimal.Parse(prixSnowboard);
                    string stockSnowboard = SQLiteReader["Stock"].ToString();
                    snowboardListe.Add(new SnowboardRequete(nomSnowboard,marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardDecimal, Convert.ToInt32(stockSnowboard))); // ADD Snowboard ITEM IN LIST
                }
                SQLiteReader.Close(); // FERMETURE READER
            }
            return snowboardListe;
        }
        public static void SQLiteAddMarque(string marqueInsert)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Marque_snowboard (Marque) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Marque", marqueInsert);     
                SQLiteInstert.ExecuteNonQuery();                      
            }
        }
        public static void SQLiteAddStyle(string marqueInsert)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Style", marqueInsert);
                SQLiteInstert.ExecuteNonQuery();
            }
        }
        public static UtilisateurConnexion SQLiteConnexion(string username, string password)
        {
            // DEF VARIABLE
            UtilisateurConnexion utilisateurConnexion = new UtilisateurConnexion(string.Empty, string.Empty, string.Empty);

            // DEF SQL
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // SQL QUERY
                string querySelectUser = "SELECT Nom,Prenom,Groupe FROM Employe WHERE Login = @login AND Password = @password LIMIT 1;";
                // SQL QUERY USERNAME
                SQLiteCommand SQLiteCommandUser = new SQLiteCommand(querySelectUser, SQLiteConn);
                SQLiteConn.Open();
                SQLiteCommandUser.Parameters.AddWithValue("login", username);
                SQLiteCommandUser.Parameters.AddWithValue("password", password);

                using (SQLiteDataReader SQLiteReaderUser = SQLiteCommandUser.ExecuteReader())
                {
                    if (SQLiteReaderUser.Read())
                    {
                        utilisateurConnexion.Nom = SQLiteReaderUser["Nom"].ToString(); ;
                        utilisateurConnexion.Prenom = SQLiteReaderUser["Prenom"].ToString();
                        utilisateurConnexion.Groupe = SQLiteReaderUser["Groupe"].ToString();

                    }
                    else
                        return null;
                return utilisateurConnexion;
                }
            }
              
        }
        public static ObservableCollection<Employe> SQLiteListUsers()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"]; // CONNECTION STRING
            ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Nom, Prenom, Login, Password, Groupe FROM Employe ", SQLiteConn);
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                { 
                    string nomEmploye = SQLiteReader["Nom"].ToString();
                    string prenomEmploye = SQLiteReader["Prenom"].ToString();
                    string loginEmploye = SQLiteReader["Login"].ToString();
                    string passwordEmploye = SQLiteReader["Password"].ToString();
                    string groupeEmploye = SQLiteReader["Groupe"].ToString();
                    usersList.Add(new Employe(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye));
                }
                SQLiteReader.Close();
                
            }
            return usersList;
        }
        //Ajout d'utilisateur
        public static bool SQLiteAddUser(Employe employe)
        {
            // CONNEXION BDD
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteConn.Open();

                // VERIFICATION USERS
                string queryVerifUser = "SELECT Login FROM Employe WHERE Login = @login";
                SQLiteCommand SQLiteVerificationUser = new SQLiteCommand(queryVerifUser, SQLiteConn);
                SQLiteVerificationUser.Parameters.AddWithValue("@login", employe.Login);

                using (SQLiteDataReader SQLiteReadUser = SQLiteVerificationUser.ExecuteReader())
                {
                    if (!SQLiteReadUser.Read())
                    {

                        // Generation Salt
                        byte[] salt = HashingPassword.SaltGeneration();
                        // Convert en byte array le password
                        byte[] employePasswordByte = Encoding.UTF8.GetBytes(employe.Password);
                        // Creation Hash a partir du password et du salt
                        employePasswordByte = HashingPassword.HashPasswordSalt(employe.Password, salt);
                        // SQL INSERT
                        string queryInsert = "INSERT INTO Employe (Nom,Prenom,Login,Password,Groupe,Salt) VALUES (?,?,?,?,?,?)";
                        SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                        SQLiteInsert.Parameters.AddWithValue("@Nom", employe.Nom);
                        SQLiteInsert.Parameters.AddWithValue("@Prenom", employe.Prenom);
                        SQLiteInsert.Parameters.AddWithValue("@Login", employe.Login);
                        SQLiteInsert.Parameters.AddWithValue("@Password", employePasswordByte);
                        SQLiteInsert.Parameters.AddWithValue("@Groupe", employe.Groupe);
                        SQLiteInsert.Parameters.AddWithValue("@Salt", salt);
                        SQLiteInsert.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
        }
        public static UtilisateurConnexion SQLiteConnexionHash(string username, string password)
        {
            // DEF VARIABLE
            UtilisateurConnexion utilisateurConnexion = new UtilisateurConnexion(string.Empty, string.Empty, string.Empty);

            // DEF SQL
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryConnexion = "SELECT Salt,Nom,Prenom,Groupe,Password FROM Employe WHERE Login = @login LIMIT 1;";
                SQLiteCommand SQLiteCommandUser = new SQLiteCommand(queryConnexion, SQLiteConn);

                SQLiteConn.Open();
                SQLiteCommandUser.Parameters.AddWithValue("login", username);


                using (SQLiteDataReader SQLiteReaderUser = SQLiteCommandUser.ExecuteReader())
                {
                    if (SQLiteReaderUser.Read())
                    {
                        byte[] saltVerification =(byte[])SQLiteReaderUser["Salt"]; // fonction pour convert en byte ????
                        byte[] passwordVerification = (byte[])SQLiteReaderUser["Password"]; // fonction pour convert en byte ????
                        byte[] hashTest = HashingPassword.HashPasswordSalt(password, saltVerification);
                        if (hashTest.SequenceEqual(passwordVerification))
                        {
                            utilisateurConnexion.Nom = (string)SQLiteReaderUser["Nom"];
                            utilisateurConnexion.Prenom = (string)SQLiteReaderUser["Prenom"];
                            utilisateurConnexion.Groupe = (string)SQLiteReaderUser["Groupe"];
                        }
                        else return null;

                    }
                    else
                        return null;
                    return utilisateurConnexion;
                }
            }

        }
        public static bool SQLiteDeleteUser(string login)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                // TODO: AJOUTER VERIF QU'IL RESTE UN ADMIN
                string queryDeleteUser = "DELETE FROM Employe WHERE Login = @login LIMIT 1;";
                SQLiteCommand SQLiteCommandDeleteUser = new SQLiteCommand(queryDeleteUser, SQLiteConn);
                SQLiteCommandDeleteUser.Parameters.AddWithValue("login", login);
                SQLiteConn.Open();

                SQLiteCommandDeleteUser.ExecuteNonQuery();
                return true;
            }

            return false;
        }
    }
} 
