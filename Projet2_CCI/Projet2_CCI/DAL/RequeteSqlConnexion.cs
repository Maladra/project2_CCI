using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    class RequeteSqlConnexion
    {
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
    }
}
