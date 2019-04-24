using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet2_CCI;
using Projet2_CCI.DAL;
using Projet2_CCI.Donnee;

namespace Project2_CCITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInsertMarque()
        {
            string marque = "Marque Test";
            RequeteSqlMarque.SQLiteAddMarque(marque);
            List<Marque> listMarque = RequeteSqlMarque.SqlReadMarque();
            Assert.IsTrue(listMarque.Any(x => x.Nom == marque));
        }

        [TestMethod]
        public void TestCreationLocation()
        {
            Client client = new Client("Gertrude", "Pauline", "06874758421");
            DateTime dateDebut = new DateTime();
            DateTime dateFin = new DateTime(2019, 04, 24);
            Marque marque = new Marque(1000, "marque test");
            RequeteSqlMarque.SQLiteAddMarque(marque.Nom);

            Genre genre = new Genre(1000, "Homme");

            Niveau niveau = new Niveau(1000, "Intermediaire");

            Style style = new Style(1000, "style test");
            RequeteSqlStyle.SQLiteAddStyle(style.Nom);

            SnowboardRequeteId snowboardRequeteId = new SnowboardRequeteId(10000, "snowboard test", marque, genre, niveau, style, (decimal)20.6, (decimal)30.6, 200);
            List<SnowboardRequeteId> listSnowboardRequeteId = new List<SnowboardRequeteId>();
            listSnowboardRequeteId.Add(snowboardRequeteId);
            LocationAvecListeSnowboard location = new LocationAvecListeSnowboard(client, "Carte Bleue", dateDebut, dateFin, (decimal)19.6, "En cours", listSnowboardRequeteId);

            RequeteSqlLocation.insertLocationSnowboard(location);
            Assert.IsTrue(listSnowboardRequeteId.Any(x => x.Nom == snowboardRequeteId.Nom));
        }

        [TestMethod]
        public void TestInsertUseretConnexion()
        {
            Employe employe = new Employe("Germain", "Laurent", "Laurent", "monPassword", "Vendeur");
            RequeteSqlUser.SQLiteAddUser(employe);

            var testConnexion = RequeteSqlConnexion.SQLiteConnexionHash(employe.Login, employe.Password);

            Assert.IsNotNull(testConnexion);

        }
    }
}
