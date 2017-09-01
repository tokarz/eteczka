using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatWydzialy
    {
        public string Wydzial { get; set; }
        public string Nazwa { get; set; }
        public DateTime Datamodify { get; set; }
        public string Idoper { get; set; }
        public string Idakcept { get; set; }
        public DateTime Dataakcept { get; set; }
        public string Firma { get; set; }
        public string Systembazowy { get; set; }
        public bool Usuniety { get; set; }
    }
}
