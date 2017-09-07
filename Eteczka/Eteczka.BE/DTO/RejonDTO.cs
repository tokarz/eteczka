using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class RejonDTO
    {
        public string Rejon { get; set; }
        public string Nazwa { get; set; }
        public string Idoper { get; set; }
        public string Idakcept { get; set; }
        public string Firma { get; set; }
        public DateTime Dataakcept { get; set; }
        public DateTime Datamodify { get; set; }
        public string Mnemonik { get; set; } //-- Mnemonik, bo rejon nazwywa sie np. R1 a chcemy zeby to bylo bardziej czytelne, np BG - Bogdanowice
        public string Systembazowy { get; set; }
        public bool Usuniety { get; set; }
    }
}
