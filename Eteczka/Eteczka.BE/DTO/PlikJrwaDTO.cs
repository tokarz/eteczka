using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class PlikJrwaDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DataUtworzenia { get; set; }
        public DateTime DataModyfikacji { get; set; }
        public PlikJrwaDTO Substruktura { get; set; }
    }
}
