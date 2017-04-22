using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatDzialy
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
        public string Datamodify { get; set; }
        public long Idoper { get; set; }
        public long Idakcept { get; set; }
        public DateTime Dataakcept { get; set; }
    }
}
