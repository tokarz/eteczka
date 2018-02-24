using System;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Model
{
    public class SessionDetails
    {
        public string IdSesji { get; set; }
        public DateTime PoczatekSesji { get; set; }
        public DateTime OstatniaAktywnoscSesji { get; set; }
        public bool SesjaAktywna { get; set; }
        public List<string> Firmy { get; set; }
        public KatLoginyFirmy AktywnaFirma { get; set; }
        public List<KatLoginyFirmy> WszystkieFirmy { get; set; }
        public bool IsAdmin {get; set;}
        public string UserWaitingroom { get; set; }
    }
}
