using Projet2_CCI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet2_CCI.DAL
{
    public class RequeteSqlClient
    {
        public static List<ClientRequete> SQLiteListClient()
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            List<ClientRequete> clientList = new List<ClientRequete>();
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Id_client,Nom,Prenom,Numero_telephone" +
                    " FROM Client ", SQLiteConn);
                SQLiteCommand.Connection.Open();
                using (SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader())
                {
                    while (SQLiteReader.Read())
                    {
                        long idClient = (long)SQLiteReader["Id_client"];
                        string nomClient = (string)SQLiteReader["Nom"];
                        string prenomClient = (string)SQLiteReader["Prenom"];
                        string numeroTelephoneClient = (string)SQLiteReader["Numero_telephone"];

                        clientList.Add(new ClientRequete(idClient,nomClient, prenomClient, numeroTelephoneClient));
                    }
                    SQLiteReader.Close();
                }
            }
            return clientList;
        }

        public static ClientRequete SQLiteSelectClient(long selectedIdClient)
        {
            string connString = ConfigurationManager.AppSettings["connectionString"];
            using (SQLiteConnection SQLiteConn = new SQLiteConnection(connString))
            {
                SQLiteCommand SQLiteCommand = new SQLiteCommand("SELECT Id_client,Nom,Prenom,Numero_telephone" +
                    " FROM Client " +
                    "WHERE Id_client=@idClient LIMIT 1", SQLiteConn);
                SQLiteCommand.Connection.Open();
                SQLiteCommand.Parameters.AddWithValue("@idClient", selectedIdClient);

                using (SQLiteDataReader SQLiteReader = SQLiteCommand.ExecuteReader())
                {
                    if (SQLiteReader.Read())
                    {
                        long idClient = (long)SQLiteReader["Id_client"];
                        string nomClient = (string)SQLiteReader["Nom"];
                        string prenomClient = (string)SQLiteReader["Prenom"];
                        string numeroTelephoneClient = (string)SQLiteReader["Numero_telephone"];
                        return new ClientRequete(idClient, nomClient, prenomClient, numeroTelephoneClient);
                    }
                    else
                    {
                        throw new ArgumentException("Id is bullshit", nameof(selectedIdClient));
                    }
                }
            }
        }
    }
}
