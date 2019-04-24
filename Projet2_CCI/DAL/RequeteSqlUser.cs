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

    /// <summary>
    /// Erreur retournée si le login utilisateur est déja présent dans la DB pendant l'insert d'un utilisateur ou son édition
    /// </summary>
    [Serializable]
    public class LoginExistentException : Exception
    {
        public LoginExistentException() : this("Le login est déja présent") { }
        public LoginExistentException(string message) : base(message) { }
        public LoginExistentException(string message, Exception inner) : base(message, inner) { }
        protected LoginExistentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class RequeteSqlUser
    {
        /// <summary>
        /// Retourne une liste des utilisateurs
        /// </summary>
        public static ObservableCollection<Employe> SQLiteListUsers()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            ObservableCollection<Employe> usersList = new ObservableCollection<Employe>();
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Nom, Prenom, Login, Password, Groupe FROM Employe ", SQLiteConn);
                SQLiteCommand.Connection.Open();
                SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader();
                while (SQLiteReader.Read())
                {
                    string nomEmploye = (string)SQLiteReader["Nom"];
                    string prenomEmploye = (string)SQLiteReader["Prenom"];
                    string loginEmploye = (string)SQLiteReader["Login"];
                    byte[] passwordEmploye = (byte[])SQLiteReader["Password"];
                    string groupeEmploye = (string)SQLiteReader["Groupe"];
                    usersList.Add(new Employe(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye));
                }
                SQLiteReader.Close();

            }
            return usersList;
        }

        /// <summary>
        /// Prend un objet Employe et l'ajoute dans la BD
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
                // TODO: using sur la commande
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
                        // TODO: using sur la commande
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
        /// Prend 2 classes Employe en parametre et update dans la BD, verifie que le login est toujours unique
        /// </summary>
        public static void SQLiteEditUser(Employe employeBefore, Employe employeAfter)
        {

            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteConn.Open();


                string queryVerifUser = "SELECT Login FROM Employe WHERE Login = @loginAfter LIMIT 1;";
                SQLiteCommand SQLiteVerificationUser = new SQLiteCommand(queryVerifUser, SQLiteConn);
                SQLiteVerificationUser.Parameters.AddWithValue("@loginAfter", employeAfter.Login);

                bool existe;
                using (SQLiteDataReader SQLiteReadUser = SQLiteVerificationUser.ExecuteReader())
                    existe = SQLiteReadUser.Read();

                // Avec modification de password
                if (!string.IsNullOrEmpty(employeAfter.Password))
                {

                    byte[] salt = HashingPassword.SaltGeneration();
                    byte[] employePasswordByte = Encoding.UTF8.GetBytes(employeAfter.Password);
                    employePasswordByte = HashingPassword.HashPasswordSalt(employeAfter.Password, salt);


                    if (!existe)
                    {
                        string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Login=@loginAfter, Password=@Password, Groupe=@Groupe ,Salt=@Salt WHERE Login=@LoginBefore;";
                        SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                        SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                        SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                        SQLiteInsert.Parameters.AddWithValue("@Password", employePasswordByte);
                        SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                        SQLiteInsert.Parameters.AddWithValue("@Salt", salt);
                        SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
                        SQLiteInsert.Parameters.AddWithValue("@LoginAfter", employeAfter.Login);
                        SQLiteInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        if (employeBefore.Login == employeAfter.Login)
                        {
                            string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Password=@Password, Groupe=@Groupe ,Salt=@Salt WHERE Login=@LoginBefore;";
                            SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                            SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                            SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                            SQLiteInsert.Parameters.AddWithValue("@Password", employePasswordByte);
                            SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                            SQLiteInsert.Parameters.AddWithValue("@Salt", salt);
                            SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
                            SQLiteInsert.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new LoginExistentException();
                        }
                    }
                }

                // Sans modification de password
                else
                {
                    if (!existe)
                    {
                        string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Login=@loginAfter, Groupe=@Groupe WHERE Login=@LoginBefore;";
                        SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                        SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                        SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                        SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                        SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
                        SQLiteInsert.Parameters.AddWithValue("@LoginAfter", employeAfter.Login);
                        SQLiteInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        if (employeBefore.Login == employeAfter.Login)
                        {
                            string queryInsert = "UPDATE Employe SET Nom=@Nom, Prenom=@Prenom, Groupe=@Groupe WHERE Login=@LoginBefore;";
                            SQLiteCommand SQLiteInsert = new SQLiteCommand(queryInsert, SQLiteConn);
                            SQLiteInsert.Parameters.AddWithValue("@Nom", employeAfter.Nom);
                            SQLiteInsert.Parameters.AddWithValue("@Prenom", employeAfter.Prenom);
                            SQLiteInsert.Parameters.AddWithValue("@Groupe", employeAfter.Groupe);
                            SQLiteInsert.Parameters.AddWithValue("@LoginBefore", employeBefore.Login);
                            SQLiteInsert.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new LoginExistentException();

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prend un string (le login de l'utilisateur) et supprime l'utilisateur de la BD
        /// vérifie qu'il reste au moins 1 utilisateur admin
        /// </summary>
        public static string SQLiteDeleteUser(Employe employe)
        {
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
        }

        /// <summary>
        /// Prend un login utilisateur en parametre et change le password associé au login
        /// </summary>
        public static bool SQLiteChangePassword(string login, string password)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];

            // Generation Salt
            byte[] salt = HashingPassword.SaltGeneration();
            // Convert en byte array le password
            byte[] employePasswordByte = Encoding.UTF8.GetBytes(password);
            // Creation Hash a partir du password et du salt
            employePasswordByte = HashingPassword.HashPasswordSalt(password, salt);




            using (SQLiteConnection sqlConn = new SQLiteConnection(connString))
            {
                string requeteChangePassword = "UPDATE Employe" +
                    " set Password = @password, Salt=@salt WHERE Login = @login";

                sqlConn.Open();
                using (SQLiteCommand sqliteCommande = new SQLiteCommand(requeteChangePassword, sqlConn))
                {
                    sqliteCommande.Parameters.AddWithValue("@login", login);
                    sqliteCommande.Parameters.AddWithValue("@password", employePasswordByte);
                    sqliteCommande.Parameters.AddWithValue("@Salt", salt);
                    sqliteCommande.ExecuteNonQuery();
                }
            }
            return true;
        }

    }
}
