using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Web.Mvc;
using Eteczka.BE.Services;



namespace Eteczka.BE.Controllers
{
    public class KatDokumentyRodzajController : Controller
    {
        private KatDokumentyRodzajService _KatDokumentyRodzajService;

        public KatDokumentyRodzajController (KatDokumentyRodzajService KatDokumentyRodzajService)
        {
            this._KatDokumentyRodzajService = KatDokumentyRodzajService;
        }

        public ActionResult PobierzWszystkieRodzajeDokumentow()
        {

            List<KatDokumentyRodzaj> PobraneDokumenty = _KatDokumentyRodzajService.PobierzRodzDok();

            return Json(new
            {
                PobraneDokumenty = PobraneDokumenty
                
            }, JsonRequestBehavior.AllowGet);

        }
           

    }
}
