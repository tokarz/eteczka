using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class PodWydzialDTO
    {
        public string Podwydzial { get; set; }
        public string Nazwa { get; set; }
        public string Wydzial { get; set; }
        public DateTime Datamodify { get; set; }
        public string Idoper { get; set; }
        public string Idakcept { get; set; }
        public DateTime Dataakcept { get; set; }
        public string Firma { get; set; }
        public string SystemBazowy { get; set; }
        public bool Usuniety { get; set; }
    }
}
