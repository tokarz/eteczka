using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatKategorieAkt
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
        public long Idoper { get; set; }
        public DateTime datamodify { get; set; }
        public long Idakcept { get; set; }
        public DateTime dataakcept { get; set; }
    }
}
