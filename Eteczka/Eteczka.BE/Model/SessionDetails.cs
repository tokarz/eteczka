using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Model
{
    public class SessionDetails
    {
        public string IdSesji { get; set; }
        public DateTime PoczatekSesji { get; set; }
        public DateTime OstatniaAktywnoscSesji { get; set; }
        public bool SesjaAktywna { get; set; }
    }
}
