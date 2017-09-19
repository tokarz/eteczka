using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class MiejscePracy
    {
        
        public string Firma { get; set; }// character(20), -- Symbol firmy
        public string Rejon { get; set; }// character(20), -- Symbol rejonu w ramach firmy
        public string Wydzial { get; set; }// character(20), -- Symbol dziaĹ‚u
        public string Podwydzial { get; set; }// character(20), -- Symbol podwydziaĹ‚u
        public string Konto5 { get; set; }// character(20), -- Symbol konta ksiÄ™gowego
        public DateTime DataPocz { get; set; }// character(10) NOT NULL, -- Data poczÄ…tkowa w miejscu pracy
        public DateTime DataKoniec { get; set; }// character(10), -- Data koĹ„cowa w miejscu pracy
        public string IdOper { get; set; }// character(30), -- ID operatora
        public string IdAkcept { get; set; }// character(30), -- Identyfikator osoby akceptujÄ…cej
        public DateTime DataModify { get; set; }// timestamp without time zone,
        public DateTime DataAkcept { get; set; }// timestamp without time zone,
        public string NumerEad { get; set; }// character(20) NOT NULL,
        public string SystemBazowy { get; set; }
        public bool Usuniety { get; set; }
        public long Id { get; set; }
    }
}
