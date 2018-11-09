using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class MiejscePracyDlaPracownika
    {
        public string DataPocz { get; set; }
        public string DataKoniec { get; set; }
        public string Firma { get; set; }
        public string Rejon { get; set; }
        public string Wydzial { get; set; }
        public string Podwydzial { get; set; }
        public string Konto5 { get; set; }
        public long Id { get; set;}
    }
}
