using System.Collections.Generic;

namespace Eteczka.Model.DTO
{
    public class StanPlikow
    {
        public List<MetaDanePliku> PlikiWSystemie { get; set; }
        public List<string> PlikiPozaSystemem { get; set; }
    }
}
