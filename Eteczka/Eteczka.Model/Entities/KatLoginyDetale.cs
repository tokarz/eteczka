using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class KatLoginyDetale
    {
        public string Identyfikator { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public Uprawnienia Uprawnienia { get; set; }
        public string UprawnieniaStr
        {
            get
            {
                return this.Uprawnienia.ToString();
            }
        }
        public DateTime DataModify { get; set; }
        public bool Usuniety { get; set; }
        public int Confidential { get; set; }
        public string KodKierownik { get; set; }
    }
}
