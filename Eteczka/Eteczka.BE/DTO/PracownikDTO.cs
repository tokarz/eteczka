using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class PracownikDTO
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PESEL { get; set; }
        public string Numeread { get; set; }
        public string Kraj { get; set; }
        public string NazwiskoRodowe { get; set; }

        public string ImieMatki { get; set; }
        public string ImieOjca { get; set; }
        public string PeselInny { get; set; }
        public string IdOper { get; set; }
        public string IdAkcept { get; set; }
        public DateTime DataModify { get; set; }
        public DateTime DataAkcept { get; set; }
        public string DataUrodzenia { get; set; }
        public string Imie2 { get; set; }
        public string SystemBazowy { get; set; }
        public bool Usuniety { get; set; }
    }
}
