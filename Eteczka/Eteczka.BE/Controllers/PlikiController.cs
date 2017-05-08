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
    }
}
