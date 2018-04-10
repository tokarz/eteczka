using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class FiltryPlikow
    {
        public Pracownik Pracownik { get; set; }
        public KatRejony Rejon { get; set; }
        public KatWydzialy Wydzial { get; set; }
        public KatPodWydzialy Podwydzial { get; set; }
        public KatKonto5 Konto5 { get; set; }
        public KatDokumentyRodzaj Typ { get; set; }
        public FiltryDat DateRange { get; set; }



    }
}
