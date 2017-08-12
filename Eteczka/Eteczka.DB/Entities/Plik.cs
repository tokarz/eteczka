using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class Plik
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
        public string PelnaSciezka { get; set; }
        public string Jrwa { get; set; }
        public string JrwaId { get; set; }
        public string Komentarz { get; set; }
        public DateTime DataUtworzenia { get; set; }
        public DateTime DataModyfikacji { get; set; }
        public string TypDokumentu { get; set; }
        public string Pesel { get; set; }
    }
}
