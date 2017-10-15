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
        public string AktywnaFirma { get; set; }
        public List<string> Firmy { get; set; }
        public KatLoginyDetale AktywnyUser { get; set; }
        public List<KatLoginyDetale> WszystkieDetale { get; set; }
        public bool IsAdmin {get; set;}
    }
}
