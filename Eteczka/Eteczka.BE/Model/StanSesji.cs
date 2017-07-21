using Eteczka.BE.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Model
{
    public class StanSesji
    {
        private static Dictionary<string, SessionDetails> OTWARTE_SESJE = new Dictionary<string, SessionDetails>();

        public bool CzySesjaJestOtwarta(string idSesji)
        {
            return OTWARTE_SESJE.ContainsKey(idSesji);
        }

        public void AktualizujSesje(string idSesji)
        {
            if (OTWARTE_SESJE.ContainsKey(idSesji))
            {
                OTWARTE_SESJE[idSesji].OstatniaAktywnoscSesji = DateTime.Now;
            }
        }

        public bool DodajSesje(string idSesji)
        {
            bool result = true;
            if (OTWARTE_SESJE.ContainsKey(idSesji))
            {
                result = false;
            }
            SessionDetails nowaSesja = new SessionDetails()
            {
                IdSesji = idSesji,
                PoczatekSesji = DateTime.Now,
                OstatniaAktywnoscSesji = DateTime.Now,
                SesjaAktywna = true
            };
            OTWARTE_SESJE.Add(idSesji, nowaSesja);

            return result;
        }

        public bool ZamknijSesje(string idSesji)
        {
            bool result = false;
            if (OTWARTE_SESJE.ContainsKey(idSesji))
            {
                SessionDetails zamkniecieSesji = new SessionDetails()
                {
                    IdSesji = idSesji,
                    PoczatekSesji = OTWARTE_SESJE[idSesji].PoczatekSesji,
                    OstatniaAktywnoscSesji = DateTime.Now,
                    SesjaAktywna = false
                };
                OTWARTE_SESJE[idSesji] = zamkniecieSesji;
                result = true;
            }

            return result;
        }

    }
}
