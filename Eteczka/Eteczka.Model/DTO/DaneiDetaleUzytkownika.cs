using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.Model.DTO
{
    public class DaneiDetaleUzytkownika
    {
        public KatLoginyDetale Detale { get; set; }
        public List<KatLoginyFirmy> Firmy { get; set; }

        public override bool Equals(object obj)
        {
            var uzytkownik = obj as DaneiDetaleUzytkownika;
            return uzytkownik != null &&
                   EqualityComparer<KatLoginyDetale>.Default.Equals(Detale, uzytkownik.Detale);
        }

        public override int GetHashCode()
        {
            return -1690766738 + EqualityComparer<KatLoginyDetale>.Default.GetHashCode(Detale);
        }
    }
}
