using System.Configuration;

namespace Eteczka
{
    public class EteczkaConfig
    {
        public static string DbHost { get; private set; }
        public static string DbName { get; private set; }
        public static string DbPort { get; private set; }

        public static void InitSystem()
        {
            DbHost = ConfigurationManager.AppSettings["dbhost"];
            DbName = ConfigurationManager.AppSettings["dbname"];
            DbPort = ConfigurationManager.AppSettings["dbport"];

        }

    }
}