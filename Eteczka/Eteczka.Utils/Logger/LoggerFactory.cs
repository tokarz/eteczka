namespace Eteczka.Utils.Logger
{
    public class LoggerFactory
    {
        private LoggerFactory() { }
        private static IEadLogger Logger;

        public static IEadLogger GetLogger()
        {
            if (Logger == null)
            {
                Logger = new EadLogger();
            }

            return Logger;
        }

    }
}
