using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class DaneUzytkownika
    {
        public string Identyfikator { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string Email { get; set; }
        public bool Usuniety { get; set; }

        public override bool Equals(object obj)
        {
            var uzytkownika = obj as DaneUzytkownika;
            return uzytkownika != null &&
                   Identyfikator == uzytkownika.Identyfikator;
        }

        public override int GetHashCode()
        {
            return -1341569154 + EqualityComparer<string>.Default.GetHashCode(Identyfikator);
        }
    }
}
