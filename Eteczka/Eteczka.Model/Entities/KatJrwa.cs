using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class KatJrwa
    {
        public long Id { get; set; }
        public string Slklas1 { get; set; }
        public string Slklas2 { get; set; }
        public string Slklas3 { get; set; }
        public string Slklas4 { get; set; }
        public string Kategoria { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public long Idoper { get; set; }
        public DateTime Datamodify { get; set; }
        public long Idakcept { get; set; }
        public DateTime Dataakcept { get; set; }
    }
}
