using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Utils.Common.DTO
{
    public class LogTabela
    {
        public string CzasWiadomosci { get; set; }
        public string User { get; set; }
        public string Firma { get; set; }
        public Akcja Akcja { get; set; }
        public string NazwaTabeli { get; set; }
        public string TabelaPrzed { get; set; }
        public string TabelaPo { get; set; }
        public bool Sucess { get; set; }
        public string Wiadomosc { get; set; }
        public string System { get; set; }


        public string ToJsonFormat()
        {
            return "{" + string.Format("\"ActionTime\" : \"{0}\", \"UserId\" : \"{1}\", \"Firm\" :\"{2}\", \"Action\" : \"{3}\",\"TableName\" : \"{4}\",\"Changes\" : {5}, \"TableBefore\" : {6}, \"Sucess\" : {7}, \"Message\" : \"{8}\", \"System\" : \"{9}\""
                , CzasWiadomosci.Trim(), User.Trim(), Firma.Trim(), Akcja, NazwaTabeli, TabelaPo, TabelaPrzed, Sucess.ToString().ToLower(), Wiadomosc, System) + "};";


        }
    }

    
        
    
}
 