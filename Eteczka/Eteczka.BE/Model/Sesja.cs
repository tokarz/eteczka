using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Model
{
    public static class Sesja
    {
        public static string FIRMA { get; set; }
        private static StanSesji STAN_SESJI =  new StanSesji();

        public static string UtworzSesje()
        {
            string result = "";
            string sessionId = "??";
            if (STAN_SESJI.DodajSesje(sessionId))
            {
                result = sessionId;
            }

            return result;
        }

        public static StanSesji PobierzStanSesji(string sessionID)
        {
            return STAN_SESJI;   
        }
        public static void UtworzLubAktualizujSesje(string sessionID)
        {
            if(STAN_SESJI.CzySesjaJestOtwarta(sessionID))
            {
                STAN_SESJI.AktualizujSesje(sessionID);
            }
            else
            {
                STAN_SESJI.DodajSesje(sessionID);
            }
        }
        public static void ZamknijSesje(string sessionID)
        {
            STAN_SESJI.ZamknijSesje(sessionID);
        }

    }
}
