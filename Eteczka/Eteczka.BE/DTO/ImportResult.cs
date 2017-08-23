using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class ImportResult
    {
        public int IloscZaimportowanychPlikow { get; set; }
        public List<string> ZaimportowanePliki { get; set; }
        public List<string> NierozpoznanePliki { get; set; }
        public bool ImportSukces { get; set; }
        public int CountImportJson { get; set; }
        public int CountImportDb { get; set; }
    }
}
