using System.Collections.Generic;

namespace Eteczka.Model.DTO
{
    public class DaneEmail
    {
        public string Adresaci { get; set; }
        public string AdresaciCc { get; set; }
        public List<string> Zalaczniki { get; set; }
        public string HasloDoZip { get; set; }
        public string Temat { get; set; }
        public string Wiadomosc { get; set; }
    }
}
