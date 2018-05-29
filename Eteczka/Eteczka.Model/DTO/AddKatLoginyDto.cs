using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.DTO
{
    public class AddKatLoginyDto : IToLogSerializer
    {
        public string Identyfikator { get; set; }
        public string Hasloshort { get; set; }
        public string Haslolong { get; set; }
        public bool IsAdmin { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string Email { get; set; }
        public bool Usuniety { get; set; }

        public IToLogSerializer WykluczPolaZHaslem(object ob)
        {
            IToLogSerializer obiektDoLogu = new AddKatLoginyDto()
            {
                Identyfikator = Identyfikator,
                Hasloshort = Hasloshort != null ? "****" : null,
                Haslolong = Haslolong != null ? "****" : null,
                IsAdmin = IsAdmin,
                Nazwisko = Nazwisko,
                Imie = Imie,
                Email = Email,
                Usuniety = Usuniety
            };
            return obiektDoLogu;
        }
    }
}
