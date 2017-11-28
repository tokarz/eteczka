using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class PracownicyZFirmamiDTO
    {
        public string Identyfikator { get; set; }
        public List<string> Firmy { get; set; }
        public int Confidential { get; set; }
    }
}
