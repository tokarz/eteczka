using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class Pliki
    {
        public long Id { get; set; }
        public string Firma { get; set; }
        public string NumerEad { get; set; }
        public string Symbol { get; set; } // rodzaj dokumentu
        public DateTime DataSkanu { get; set; }
        public DateTime DataDokumentu { get; set; }
        public DateTime DataPocz { get; set; }
        public DateTime DataKoniec { get; set; }
        public string NazwaScan { get; set; }
        public string NazwaEad { get; set; }
        public string PelnasciezkaEad { get; set; }
        public string TypPliku { get; set; }
        public string OpisDodatkowy { get; set; }
        public bool DokumentWlasny { get; set; }
        public string IdOper { get; set; }
        public string IdAkcept { get; set; }
        public DateTime DataModyfikacji { get; set; }
        public DateTime DataAkcept { get; set; }
        public string Systembazowy { get; set; }//EAD
        public bool Usuniety { get; set; }
    }
}
