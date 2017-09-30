using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Eteczka.BE.Model
{
    public class StanSesji
    {
        private static Dictionary<string, SessionDetails> OTWARTE_SESJE = new Dictionary<string, SessionDetails>();
        private static Dictionary<string, SessionDetails> ZAMKNIETE_SESJE = new Dictionary<string, SessionDetails>();

        public bool CzySesjaJestOtwarta(string idSesji)
        {
            bool result = false;
            if (idSesji != null && OTWARTE_SESJE.ContainsKey(idSesji))
            {
                SessionDetails daneSesji = OTWARTE_SESJE[idSesji];
                bool timeoutSesjiMinal = (DateTime.Now.Subtract(daneSesji.OstatniaAktywnoscSesji) <= TimeSpan.FromMinutes(10));
                result = daneSesji.SesjaAktywna;
            }

            return result;
        }

        public void AktualizujSesje(string idSesji)
        {
            if (OTWARTE_SESJE.ContainsKey(idSesji))
            {
                OTWARTE_SESJE[idSesji].OstatniaAktywnoscSesji = DateTime.Now;
            }
        }

        public bool UstawAktywnaFirme(string sessionID, string company)
        {
            bool result = false;
            lock (OTWARTE_SESJE)
            {
                if (OTWARTE_SESJE.ContainsKey(sessionID))
                {
                    SessionDetails detaleSesji = OTWARTE_SESJE[sessionID];
                    detaleSesji.AktywnaFirma = company;
                    result = true;
                }
            }

            return result;
        }

        public SessionDetails PobierzSesje(string sessionID)
        {
            SessionDetails result = null;
            if (OTWARTE_SESJE.ContainsKey(sessionID))
            {
                result = OTWARTE_SESJE[sessionID];
            }
            return result;
        }

        public List<SessionDetails> PobierzOtwarteSesje()
        {
            List<SessionDetails> sesje = new List<SessionDetails>();

            foreach (SessionDetails detale in OTWARTE_SESJE.Values)
            {
                sesje.Add(detale);
            }

            return sesje;
        }

        public SessionDetails DodajSesje(string idSesji)
        {
            if (OTWARTE_SESJE.ContainsKey(idSesji))
            {
                return null;
            }
            SessionDetails nowaSesja = new SessionDetails()
            {
                IdSesji = idSesji,
                PoczatekSesji = DateTime.Now,
                OstatniaAktywnoscSesji = DateTime.Now,
                SesjaAktywna = true
            };
            OTWARTE_SESJE.Add(idSesji, nowaSesja);

            return nowaSesja;
        }

        public bool ZamknijSesje(string session)
        {
            bool result = false;
            if (OTWARTE_SESJE.ContainsKey(session))
            {
                SessionDetails sesja = OTWARTE_SESJE[session];

                sesja.OstatniaAktywnoscSesji = DateTime.Now;
                sesja.SesjaAktywna = false;

                ZAMKNIETE_SESJE[sesja.IdSesji] = sesja;
                OTWARTE_SESJE.Remove(sesja.IdSesji);
                result = true;
            }

            return result;
        }

    }
}
