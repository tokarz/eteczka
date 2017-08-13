using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatFirmy
    {
        public string Nip { get; set; } // Id
        public string Firma { get; set; } //-- Identyfikator firmy : np. TFG, TFNI itp.    
        public string Nazwa { get; set; }
        public string Nazwaskrocona { get; set; }
        public string Ulica { get; set; }
        public string Numerdomu { get; set; }
        public string Numerlokalu { get; set; }
        public string Miasto { get; set; }
        public string Kodpocztowy { get; set; }
        public string Poczta { get; set; }
        public string Gmina { get; set; }
        public string Powiat { get; set; }
        public string Wojewodztwo { get; set; }
        public string Regon { get; set; }
        public string Nazwa2 { get; set; }
        public string Pesel { get; set; }
        public long Idoper { get; set; }
        public long Idakcept { get; set; }
        public string Nazwisko { get; set; } //Nazwisko jesli firma prywatna
        public string Imie { get; set; } //Imie jesli firma prywatna
        public DateTime Datamodify { get; set; }
        public DateTime Dataakcept { get; set; }
        public string Lokalizacjapapier { get; set; }
    }
}
