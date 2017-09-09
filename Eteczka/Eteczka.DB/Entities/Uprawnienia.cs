using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class Uprawnienia
    {
        public bool RolaReadOnly { get; set; }
        public bool RolaAddPracownik { get; set; }
        public bool RolaModifyPracownik { get; set; }
        public bool RolaAddFile { get; set; }
        public bool RolaModifyFile { get; set; }
        public bool RolaSlowniki { get; set; }
        public bool RolaSendEmail { get; set; }
        public bool RolaRaport { get; set; }
        public bool RolaRaportExport { get; set; }
        public bool RolaDoubleAkcept { get; set; }
    }
}
