using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Nazwa { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string HasloShort { get; set; }
        public string HasloLong { get; set; }
        public DateTime DataModify { get; set; }
        public Uprawnienia Uprawnienia { get; set; }
    }
}
