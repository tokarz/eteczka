using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Controllers
{
    public class PlikiController : Controller
    {
        private IPlikiService _PlikiService;

        public PlikiController()
        {
            _PlikiService = new PlikiService();
        }

        public PlikiController(PlikiService service)
        {
            _PlikiService = service;
        }

        public ActionResult PobierzWszystkie()
        {
            List<KatTeczki> pliki = _PlikiService.PobierzWszystkie();

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzDlaPeselu(string pesel)
        {
            List<KatTeczki> result = new List<KatTeczki>();
            List<KatTeczki> wszystkiePliki = _PlikiService.PobierzWszystkie();
            foreach (KatTeczki plik in wszystkiePliki)
            {
                //if (plik.Pesel.Equals(pesel))
                //{
                result.Add(plik);
                //}
            }

            return Json(new
            {
                pliki = result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzMetadane(string plik)
        {
            MetaDanePliku meta = _PlikiService.PobierzMetadane(plik);

            return Json(new
            {
                meta = meta
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
