using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;


namespace Eteczka.BE.Controllers
{
    public class FirmyController : Controller
    {
        private IFirmyService _firmyService;

        public FirmyController(IFirmyService firmyService)
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

