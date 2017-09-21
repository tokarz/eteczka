using System;

namespace Eteczka.BE.Model
{
    public static class Sesja
    {
        private static StanSesji STAN_SESJI = new StanSesji();

        public static SessionDetails UtworzSesje()
        {
            string sessionId = Guid.NewGuid().ToString("N"); ;
            SessionDetails sesja = STAN_SESJI.DodajSesje(sessionId);

            return sesja;
        }

        public static StanSesji PobierzStanSesji()
        {
            return STAN_SESJI;
        }


        public static void UtworzLubAktualizujSesje(string sessionID)
        {
            if (STAN_SESJI.CzySesjaJestOtwarta(sessionID))
            {
                STAN_SESJI.AktualizujSesje(sessionID);
            }
            else
            {
                STAN_SESJI.DodajSesje(sessionID);
            }
        }
        public static void ZamknijSesje(string sesja)
        {
            STAN_SESJI.ZamknijSesje(sesja);
        }

    }
}
