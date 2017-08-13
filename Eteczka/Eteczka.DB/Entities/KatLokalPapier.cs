using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatLokalPapier
    {
        //PRIMARY KEY (firma, lokalpapier)  
        public string Firma { get; set; }
        public string LokalPapier { get; set; }
        public string Nazwa { get; set; }
        public string Ulica { get; set; }
        public string Numerdomu { get; set; }
        public string Numerlokalu { get; set; }
        public string Miasto { get; set; }
        public string Kodpocztowy { get; set; }
        public string Poczta { get; set; }
        public long Idoper { get; set; }
        public long Idakcept { get; set; }
        public DateTime Datamodify { get; set; }
        public DateTime Dataakcept { get; set; }
    }
}
