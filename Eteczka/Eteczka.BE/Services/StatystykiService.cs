using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.BE.Enums;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Services
{
    public class StatystykiService : IStatystykiService
    {
        private readonly string[] COLORS = new string[] { "#3366CC", "#DC3912", "#FF9900", "#109618", "#F09F18", "#EEEEEE", "#ABABAB" };

        private IPlikiService _PlikiService;

        public StatystykiService()
        {
            this._PlikiService = new PlikiService();
        }

        public List<DaneWykresowe> PobierzDaneWykresowe(TypWykresu typWykresu)
        {
            List<DaneWykresowe> result = new List<DaneWykresowe>();

            Dictionary<string, double> licznikTypow = new Dictionary<string, double>();

            List<KatTeczki> wszystkieDokumenty = _PlikiService.PobierzWszystkie();

            foreach (KatTeczki plik in wszystkieDokumenty)
            {
                if (licznikTypow.Keys.Contains(plik.TypDokumentu))
                {
                    double licznik = licznikTypow[plik.TypDokumentu];
                    licznik += 1;
                    licznikTypow[plik.TypDokumentu] = licznik;
                }
                else
                {
                    licznikTypow.Add(plik.TypDokumentu, 1);
                }
            }
            int iteration = 0;
            foreach (string typ in licznikTypow.Keys)
            {
                DaneWykresowe daneCzesc = new DaneWykresowe();
                daneCzesc.label = typ;
                double procentowaWartosc = (licznikTypow[typ] / wszystkieDokumenty.Count) * 100;
                daneCzesc.value = Math.Floor(procentowaWartosc);
                daneCzesc.color = this.COLORS[iteration++];

                result.Add(daneCzesc);
            }


            return result;
        }
    }
}
