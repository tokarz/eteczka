using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatRejony
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
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
        public string Kraj { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Krs { get; set; }
        public string Pesel { get; set; }
        public DateTime Datamodify { get; set; }
        public long Idoper { get; set; }
        public long Idakcept { get; set; }
        public DateTime dataakcept { get; set; }
        public string lokalizacjapapier { get; set; }
    }
}
