using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.Model.DTO
{
    public class DaneiDetaleUzytkownika
    {
        public DaneUzytkownika DaneUzytkownika { get; set; }
        public List<KatLoginyDetale> Detale { get; set; }

        public override bool Equals(object obj)
        {
            var uzytkownika = obj as DaneiDetaleUzytkownika;
            return uzytkownika != null &&
                   EqualityComparer<DaneUzytkownika>.Default.Equals(DaneUzytkownika, uzytkownika.DaneUzytkownika);
        }

        public override int GetHashCode()
        {
            return -1589580215 + EqualityComparer<DaneUzytkownika>.Default.GetHashCode(DaneUzytkownika);
        }
    }
}
