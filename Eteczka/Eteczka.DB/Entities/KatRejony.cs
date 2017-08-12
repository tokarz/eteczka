using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatRejony
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
        public long Idoper { get; set; }
        public long Idakcept { get; set; }
        public DateTime Dataakcept { get; set; }
        public DateTime Datamodify { get; set; }
        public string Lokalizacjapapier { get; set; }
        public long FirmaId { get; set; }
    }
}
