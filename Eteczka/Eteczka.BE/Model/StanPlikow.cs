using Eteczka.BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Model
{
    public class StanPlikow
    {
        public List<MetaDanePliku> PlikiWSystemie { get; set; }
        public List<string> PlikiPozaSystemem { get; set; }
    }
}
