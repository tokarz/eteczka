using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.Utils;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Controllers
{
    public class PracownicyController : Controller
    {
        public ActionResult PobierzWszystkich(string sessionId)
        {
            List<PracownikDTO> pracownicy = new List<PracownikDTO>();
            PracownikDTO maciek = new PracownikDTO
            {
                Id = "123",
                DataUrodzenia = "17.10.1985",
                Dzial = "IT",
                Imie = "Maciej",
                Nazwisko = "Tokarz",
                PESEL = "85101717855"
            };

            PracownikDTO paszczak = new PracownikDTO
            {
                Id = "1",
                DataUrodzenia = "27.12.1961",
                Dzial = "IT",
                Imie = "Zbigniew",
                Nazwisko = "Tokarz",
                PESEL = "Aktotowie?:)"
            };

            PracownikDTO burqin = new PracownikDTO
            {
                Id = "Pff",
                DataUrodzenia = "12.07.1410",
                Dzial = "TESTOWANIE-TESTOW",
                Imie = "Aleksandra",
                Nazwisko = "Tokarz",
                PESEL = "W 1410 nie bylo jeszcze PESELI"
            };
            for (int i = 0; i < 500; i++)
            {
                pracownicy.Add(paszczak);
                pracownicy.Add(burqin);
                pracownicy.Add(maciek);
            }

            return Json(new
            {
                data = pracownicy
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
