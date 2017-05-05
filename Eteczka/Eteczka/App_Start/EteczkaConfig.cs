using System;
using System.Configuration;
using System.IO;

namespace Eteczka
{
    public class EteczkaConfig
    {
        public static string DbHost { get; private set; }
        public static string DbName { get; private set; }
        public static string DbPort { get; private set; }
        public static string EAD_ROOT { get; private set; }

        public static void InitSystem()
        {
            DbHost = ConfigurationManager.AppSettings["dbhost"];
            DbName = ConfigurationManager.AppSettings["dbname"];
            DbPort = ConfigurationManager.AppSettings["dbport"];
            string eadRootName = ConfigurationManager.AppSettings["rootdir"];

            EAD_ROOT = System.Environment.GetEnvironmentVariable(eadRootName);

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