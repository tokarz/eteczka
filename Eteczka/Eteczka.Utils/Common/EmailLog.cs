using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Utils.Common
{
    public class EmailLog
    {
        
        public string CzasWiadomosci { get; set; }
        public string Firma { get; set; }
        public string UserId { get; set; }
        public string Adresaci { get; set; }
        public string AdresaciCc { get; set; }
        public string Temat { get; set; }
        public string Tresc { get; set; }
        public string Zalaczniki { get; set; }
        public string Status { get; set; }
        

        public string ToJsonFormat()
        {
            return  "{" + string.Format("\"Czas akcji\" : \"{0}\", \"Firma\" : \"{1}\", \"Użytkownik\" :\"{2}\", \"Adresaci\" : \"{3}\",\"AdresaciDw\" : \"{4}\", \"Temat\" : \"{5}\", \"Tresc\" : \"{6}\", \"Załączniki\" : \"{7}\", \"Status\" : \"{8}\"", CzasWiadomosci.Trim(), Firma.Trim(), UserId.Trim(), Adresaci.Trim(), AdresaciCc.Trim(), Temat.Trim(), Tresc.Trim(), Zalaczniki.Trim(), Status.Trim()) + "}";

         
        }
    }
   
}
