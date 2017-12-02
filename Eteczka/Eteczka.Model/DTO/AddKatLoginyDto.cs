using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class AddKatLoginyDto
    {
        public string Identyfikator { get; set; }
        public string Hasloshort { get; set; }
        public string Haslolong { get; set; }
        public DateTime Datamodify { get; set; }
        public bool IsAdmin { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public List<string> Firmy { get; set; }
        public string Email { get; set; }
        public Uprawnienia Uprawnienia { get; set; }
        public DateTime DataModify { get; set; }
        public bool Usuniety { get; set; }
        public int Confidential { get; set; }
        public string KodKierownik { get; set; }
    }
}
