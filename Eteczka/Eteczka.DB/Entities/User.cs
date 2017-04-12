using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PESEL { get; set; }
        public string DataUrodzenia { get; set; }
        public string Dzial { get; set; }
    }
}
