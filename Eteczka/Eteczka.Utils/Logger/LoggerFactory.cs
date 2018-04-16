using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
