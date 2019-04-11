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
    class RequeteSqlUser
    {
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
                        // a finir
                        if (string.IsNullOrEmpty(employeAfter.Password)) { }
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
                    // TODO: FAIRE PROPRE (virer les strings a mettre dans interface)
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
        /// Prend un login utilisateur en parametre et change le password associe au login
        /// </summary>
        public static bool SQLiteChangePassword(string login)
        {
            return true;
        }
        // FONCTION probablement inutile
        //public static Employe SQLiteSelectUser(string login)
        //{
        //    string connString = ConfigurationManager.AppSettings["connectionString"];
        //
        //
        //    using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
        //    {
        //        string selectUset = "SELECT Nom, Prenom, Login, Password, Groupe FROM Employe WHERE Login=@login LIMIT 1;";
        //        SQLiteCommand SQLiteCommandSelectUser = new SQLiteCommand(selectUset, SQLiteConn);
        //        SQLiteConn.Open();
        //        SQLiteCommandSelectUser.Parameters.AddWithValue("login", login);
        //        using (SQLiteDataReader dataReadUser = SQLiteCommandSelectUser.ExecuteReader())
        //        {
        //            string nomEmploye = dataReadUser["Nom"].ToString();
        //            string prenomEmploye = dataReadUser["Prenom"].ToString();
        //            string loginEmploye = dataReadUser["Login"].ToString();
        //            string passwordEmploye = dataReadUser["Password"].ToString();
        //            string groupeEmploye = dataReadUser["Groupe"].ToString();
        //
        //            Employe employe = new Employe(nomEmploye, prenomEmploye, loginEmploye, passwordEmploye, groupeEmploye);
        //
        //    
        //    return employe;
        //        }
        //    }
        //}

    }
}
