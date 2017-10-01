using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class FiltryPlikow
    {
        public string Rejon { get; set;}
        public string Wydzial { get; set; }
        public string Podwydzial { get; set; }
        public string Konto5 { get; set; }
        public string Pracownik { get; set; }

    }
}
