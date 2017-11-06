using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class KomitPliku
    {
        public string Nazwa { get; set; }
        public KatDokumentyRodzaj RodzajDokumentu { get; set; }
        public Pracownik Pracownik { get; set; }
        public bool Dokwlasny { get; set; }
        public DateTime DataWytworzenia { get; set; }
        public DateTime DataPocz { get; set; }
        public DateTime DataKoniec { get; set; }
        public string OpisDodatkowy { get; set; }
         
    }
}
