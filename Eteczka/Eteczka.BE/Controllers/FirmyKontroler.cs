using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.Utils;
using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using Eteczka.BE.Services;


namespace Eteczka.BE.Controllers
{
    public class FirmyKontroler : Controller
    {
        private IFirmyService _firmyService;

        public FirmyKontroler(IFirmyService firmyService)
        {
            this._firmyService = firmyService;
        }

        public ActionResult PobierzWszystkieFirmy()
        {
            List<FirmaDTO> PobraneFirmyDTO = _firmyService.PobierzWszystkie();


            return Json(new
            {
                Firmy = PobraneFirmyDTO
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

