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

        public StatystykiService(IPlikiService plikiService)
        {
            this._PlikiService = plikiService;
        }

        public List<DaneWykresowe> PobierzDaneWykresowe(TypWykresu typWykresu)
        {
            List<DaneWykresowe> result = new List<DaneWykresowe>();

            //    Dictionary<long, double> licznikTypow = new Dictionary<long, double>();

            //    List<Pliki> wszystkieDokumenty = _PlikiService.PobierzWszystkie();

            //    foreach (Pliki plik in wszystkieDokumenty)
            //    {
            //        if (licznikTypow.Keys.Contains(plik.Typid))
            //        {
            //            double licznik = licznikTypow[plik.Typid];
            //            licznik += 1;
            //            licznikTypow[plik.Typid] = licznik;
            //        }
            //        else
            //        {
            //            licznikTypow.Add(plik.TypPliku, 1);
            //        }
            //    }
            //    int iteration = 0;
            //    foreach (long typ in licznikTypow.Keys)
            //    {
            //        DaneWykresowe daneCzesc = new DaneWykresowe();
            //        daneCzesc.label = "" + typ;
            //        double procentowaWartosc = (licznikTypow[typ] / wszystkieDokumenty.Count) * 100;
            //        daneCzesc.value = Math.Floor(procentowaWartosc);
            //        daneCzesc.color = this.COLORS[iteration++];

            //        result.Add(daneCzesc);
            //    }


            return result;
        }
    }
}
