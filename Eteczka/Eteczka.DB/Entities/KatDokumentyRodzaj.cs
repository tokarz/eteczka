using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatDokumentyRodzaj
    {
        public long Id { get; set; }//numeric(10,0) NOT NULL, -- Numer id
        public string Symbol { get; set; } //character(20), -- Symbol dokumetu, np. SWPR - œwiadectwo pracy
        public string Nazwa { get; set; }//character(254), -- Nazwa dokumentu
    }
}
