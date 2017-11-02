using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class KatLokalPapier
    {
        public string Firma { get; set; }
        public string LokalPapier { get; set; }
        public string Nazwa { get; set; }
        public string Ulica { get; set; }
        public string Numerdomu { get; set; }
        public string Numerlokalu { get; set; }
        public string Miasto { get; set; }
        public string Kodpocztowy { get; set; }
        public string Poczta { get; set; }
        public string Idoper { get; set; }
        public string Idakcept { get; set; }
        public DateTime Datamodify { get; set; }
        public DateTime Dataakcept { get; set; }
        public string System { get; set; }
        public bool Usuniety { get; set; }
    }
}
