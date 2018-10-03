using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class PracownikZMiejscemPracy
    {
        public string Pesel { get; set; }
        public string DataUrodzenia { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string DrugieImie { get; set; }
        public string NazwiskoRodowe { get; set; }
        public string ImieMatki { get; set; }
        public string ImieOjca { get; set; }
        public string KodKierownik { get; set; }
        public int Confidential { get; set; }
        public string PeselObcy { get; set; }

        public string Firma { get; set; }
        public string Rejon  { get; set; }
        public string Wydzial { get; set; }
        public string PodWydzial { get; set; }
        public string Konto5  { get; set; }
        public DateTime DataPocz { get; set; }
        public DateTime DataKoniec { get; set; }



    }
}
