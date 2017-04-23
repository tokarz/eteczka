using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatLoginy
    {
        public long Id { get; set; }
        public string Identyfikator { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string Hasloshort { get; set; }
        public string Haslolong { get; set; }
        public bool Rolareadonly { get; set; }
        public bool Rolaaddpracownik { get; set; }
        public bool Rolamodifypracownik { get; set; }
        public bool Rolaaddfile { get; set; }
        public bool Rolamodifyfile { get; set; }
        public bool Rolaslowniki { get; set; }
        public bool Rolasendmail { get; set; }
        public bool Rolaraport { get; set; }
        public bool Rolaraportexport { get; set; }
        public bool Roladoubleakcept { get; set; }
        public DateTime Datamodify { get; set; }
    }
}
