using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class Pliki
    {
        public string Symbol { get; set; }
        public DateTime DataSkanu { get; set; }
        public DateTime DataDokumentu { get; set; }
        public DateTime DataPocz { get; set; }
        public DateTime DataKoniec { get; set; }
        public string NazwaPliku { get; set; }
        public string PelnaSciezka { get; set; }
        public string TypPliku { get; set; }
        public string OpisDodatkowy { get; set; }
        public string NumerEad { get; set; }
        public bool DokumentWlasny { get; set; }
        public string IdOper { get; set; }
        public string IdAkcept { get; set; }
        public DateTime DataModyfikacji { get; set; }
        public DateTime DataAkcept { get; set; }
    }
}
