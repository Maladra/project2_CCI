using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoadDbPath
    {
        public static void setDbPath()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["connectionString"]))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                string dbFile = "gestion.db";
                string dbDirectory = "SnowShop";
                string fullPath = Path.Combine(path, dbDirectory, dbFile);

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string Dbacces = "Data Source = " +fullPath;
                config.AppSettings.Settings["connectionString"].Value = Dbacces;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

            }
        }


    }
}
