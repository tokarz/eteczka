using System;
using System.Configuration;
using System.IO;

namespace Eteczka
{
    public static class EteczkaConfig
    {
        public static string DbUser { get; private set; }
        public static string DbPassword { get; private set; }
        public static string DbHost { get; private set; }
        public static string DbName { get; private set; }
        public static string DbPort { get; private set; }
        public static string EAD_ROOT { get; private set; }

        public static void InitSystem()
        {
            DbUser = ConfigurationManager.AppSettings["dbuser"];
            DbPassword = ConfigurationManager.AppSettings["dbpassword"];
            DbHost = ConfigurationManager.AppSettings["dbhost"];
            DbName = ConfigurationManager.AppSettings["dbname"];
            DbPort = ConfigurationManager.AppSettings["dbport"];
            string eadRootName = ConfigurationManager.AppSettings["rootdir"];

            EAD_ROOT = Environment.GetEnvironmentVariable(eadRootName);

            string configurationPath = EAD_ROOT + "/eteczka.create.txt";

            if (File.Exists(configurationPath))
            {
                using (var tw = new StreamWriter(configurationPath, true))
                {
                    tw.WriteLine("Server Startup Time: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    tw.Close();
                }
            }
            else
            {
                File.Create(configurationPath);
                using (var tw = new StreamWriter(configurationPath, true))
                {
                    tw.WriteLine("Server Startup Time: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    tw.Close();
                }
            }
        }

    }
}