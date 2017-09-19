using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class KatLoginy
    {
        public string Identyfikator { get; set; }
        public string Hasloshort { get; set; }
        public string Haslolong { get; set; }
        public DateTime Datamodify { get; set; }
        public bool IsAdmin { get; set; }
        public bool Usuniety { get; set; }
    }
}
