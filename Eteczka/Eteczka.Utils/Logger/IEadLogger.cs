﻿using Eteczka.BE.Model;
using Eteczka.Utils.Common.DTO;

namespace Eteczka.Utils.Logger
{
    public interface IEadLogger
    {
        void LOG(PoziomLogowania poziom, Akcja akcja, string wiadomosc, SessionDetails sesja = null , string numerEad = "");
        void LOG_DANE_OSOBOWE(PoziomLogowania poziom, Akcja akcja, string wiadomosc, SessionDetails sesja, object TabelaPrzed, object TabelaPo);
    }
}
