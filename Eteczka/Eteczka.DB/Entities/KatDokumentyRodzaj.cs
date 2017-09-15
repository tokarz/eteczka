using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatDokumentyRodzaj
    {
        public string Symbol { get; set; } //character(20), -- Symbol dokumetu, np. SWPR - swiadectwo pracy
        public string Nazwa { get; set; }//character(254), -- Nazwa dokumentu
        public bool Dokwlasny { get; set; }//-- Okresla czy dokument zostal wytworzony przez nas czy jest to dokument obcy True=wlasny
        public string Jrwa { get; set; }    //-- Pelna klasyfikacja JRWA character(10)
        public string Teczkadzial { get; set; } //-- Czsc akt - dozwolone wartsci : A,B,C
        public string Typedycji { get; set; } //-- Okresla pola które maja byc wymagane w edycji, np data dokumentu, data wa¿noœci itp.
        public string Idoper { get; set; }
        public string Idakcept { get; set; }
        public DateTime Datamodify { get; set; }
        public DateTime Dataakcept { get; set; }
        public string SystemBazowy {get; set;}
        public bool Usuniety { get; set; }
        public int Confidential { get; set; }
    }
}
