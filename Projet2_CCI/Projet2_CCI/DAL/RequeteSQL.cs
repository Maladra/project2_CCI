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
    /// <summary>
    /// Contient les fonctions pour les requêtes SQL
    /// </summary>
    static class SQLHelper
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
                    decimal prixSnowboardEuroDecimal = decimal.Parse(prixSnowboarEuro);
                    decimal prixSnowboardDollarDecimal = decimal.Parse(prixSnowboarDollar);
                    string stockSnowboard = SQLiteReader["Stock"].ToString();
                    snowboardListe.Add(new SnowboardRequete(nomSnowboard, marqueSnowboard, genreSnowboard, niveauSnowboard, styleSnowboard, prixSnowboardEuroDecimal, prixSnowboardDollarDecimal, Convert.ToInt32(stockSnowboard))); // ADD Snowboard to LIST
                }
                SQLiteReader.Close(); // FERMETURE READER
            }
            return snowboardListe;
        }

        /// <summary>
        /// Prend un string et fait une requete SQL pour inserer une marque de snowboard dans la BD
        /// </summary>
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

        /// <summary>
        /// Prend un string et fait une requete SQL pour inserer un style de snowboard dans la BD
        /// </summary>
        public static void SQLiteAddStyle(string styleInsert)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string queryInsert = "INSERT INTO Style_snowboard (Style) VALUES (?)";
                SQLiteConn.Open();
                SQLiteCommand SQLiteInstert = new SQLiteCommand(queryInsert, SQLiteConn);
                SQLiteInstert.Parameters.AddWithValue("@Style", styleInsert);
                SQLiteInstert.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retourne une liste des utilisateurs
        /// </summary>
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

        /// <summary>
        /// Prend un objet employe et l'ajoute dans la BD
        /// </summary>
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

        /// <summary>
        /// Prend 2 string (username et password) et test la connexion de l'utilisateur a l'application
        /// </summary>
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
                        byte[] saltVerification = (byte[])SQLiteReaderUser["Salt"];
                        byte[] passwordVerification = (byte[])SQLiteReaderUser["Password"];
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

        /// <summary>
        /// Prend un string (le login de l'utilisateur) et supprime l'utilisateur de la BD 
        /// </summary>
        public static string SQLiteDeleteUser(Employe employe)
        {
            //TODO: VERIFICATION si il reste au moins 1 administrateur :
            // selectionner user a selectionner si il est admin verifier qu'il en reste plus que 1 si oui delete si non rien faire
            // si user n'est pas admin delete
            string connString = ConfigurationManager.AppSettings["connectionString"];

            using (SQLiteConnection sqliteConn = new SQLiteConnection(connString))
            {
                string queryDeleteUser = "DELETE FROM Employe WHERE Login = @login;";
                SQLiteCommand sqliteCommandDeleteUser = new SQLiteCommand(queryDeleteUser, sqliteConn);
                sqliteCommandDeleteUser.Parameters.AddWithValue("login", employe.Login);

                string queryCountAdministrateur = "SELECT COUNT(*) FROM Employe WHERE Groupe = 'Administrateur'";
                SQLiteCommand sqliteCountAdmin = new SQLiteCommand(queryCountAdministrateur, sqliteConn);


                if (employe.Groupe == "Administrateur")
                {
                    sqliteConn.Open();
                    long count = (long)sqliteCountAdmin.ExecuteScalar();

                    if (count > 1)
                    {
                        sqliteCommandDeleteUser.ExecuteNonQuery();
                        return "Le compte a bien été supprimé.";
                    }
                    else
                    {
                        return "Il doit y avoir au moins un administrateur.";
                    }

                    


                }
                else
                {

                    sqliteConn.Open();

                    sqliteCommandDeleteUser.ExecuteNonQuery();
                    return "Le compte a bien été supprimé.";
                }
            }
            return "Erreur pendant la requête";

        }


        /// <summary>
        /// Prend 2 classes Employe en parametre et update dans la BD, verifie que le login est toujours unique
        /// </summary>
        public static bool SQLiteEditUser(Employe employeBefore, Employe employeAfter)
        {
            // CONNEXION BDD
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteConn.Open();

                // VERIFICATION USERS
                string queryVerifUser = "SELECT Login FROM Employe WHERE Login = @loginAfter LIMIT 1;";
                SQLiteCommand SQLiteVerificationUser = new SQLiteCommand(queryVerifUser, SQLiteConn);
                SQLiteVerificationUser.Parameters.AddWithValue("@loginAfter", employeAfter.Login);

                using (SQLiteDataReader SQLiteReadUser = SQLiteVerificationUser.ExecuteReader())
                {
                    if (!SQLiteReadUser.Read())
                    { 
                            // Generation Salt
                            byte[] salt = HashingPassword.SaltGeneration();
                            // Convert en byte array le password
                            byte[] employePasswordByte = Encoding.UTF8.GetBytes(employeAfter.Password);
                            // Creation Hash a partir du password et du salt
                            employePasswordByte = HashingPassword.HashPasswordSalt(employeAfter.Password, salt);
                            // SQL INSERT
                            string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Login=@loginAfter, Password=@Password, Groupe=@Groupe ,Salt=@Salt WHERE Login=@LoginBefore;";
                            SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                            SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                            SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                            SQLiteInsert.Parameters.AddWithValue("@LoginAfter", employeAfter.Login);
                            SQLiteInsert.Parameters.AddWithValue("@Password", employePasswordByte);
                            SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                            SQLiteInsert.Parameters.AddWithValue("@Salt", salt);
                            SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
                            SQLiteInsert.ExecuteNonQuery();
                            return true;
                        
                    }
                    else if (employeBefore.Login == employeAfter.Login)
                    {
                        // Generation Salt
                        byte[] salt = HashingPassword.SaltGeneration();
                        // Convert en byte array le password
                        byte[] employePasswordByte = Encoding.UTF8.GetBytes(employeAfter.Password);
                        // Creation Hash a partir du password et du salt
                        employePasswordByte = HashingPassword.HashPasswordSalt(employeAfter.Password, salt);
                        // SQL INSERT
                        string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Password=@Password, Groupe=@Groupe ,Salt=@Salt WHERE Login=@LoginBefore;";
                        SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                        SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                        SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                        SQLiteInsert.Parameters.AddWithValue("@Password", employePasswordByte);
                        SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                        SQLiteInsert.Parameters.AddWithValue("@Salt", salt);
                        SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
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


        // FONCTION probablement inutile
        public static Employe SQLiteSelectUser(string login)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];


            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                string selectUset = "SELECT Nom, Prenom, Login, Password, Groupe FROM Employe WHERE Login=@login LIMIT 1;";
                SQLiteCommand SQLiteCommandSelectUser = new SQLiteCommand(selectUset, SQLiteConn);
                SQLiteConn.Open();
                SQLiteCommandSelectUser.Parameters.AddWithValue("login", login);
                using (SQLiteDataReader dataReadUser = SQLiteCommandSelectUser.ExecuteReader())
                {
                    string nomEmploye = dataReadUser["Nom"].ToString();
                    string prenomEmploye = dataReadUser["Prenom"].ToString();
                    string loginEmploye = dataReadUser["Login"].ToString();
                    string passwordEmploye = dataReadUser["Password"].ToString();
                    string groupeEmploye = dataReadUser["Groupe"].ToString();

                    Employe employe = new Employe(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye);

            
            return employe;
                }
            }
        }
    }
}

