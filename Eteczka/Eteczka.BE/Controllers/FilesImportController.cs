using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Controllers
{
    public class FilesImportController : Controller
    {
        private IImportService _ImportService;

        public FilesImportController(IImportService importService)
        {
            _ImportService = importService;
        }

        public ActionResult ImportujStrukturePlikow(bool nadpisz)
        {
            bool success = false;

            ImportResult result = _ImportService.ImportFiles(nadpisz);


            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujLokalizacjeArchiwow(string sessionId, bool nadpisz)
        {

            bool success = false;

            ImportResult result = _ImportService.ImportArchives(nadpisz);


            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ImportujFirmy(string sessionId, bool nadpisz)
        {
            ImportResult result = _ImportService.ImportFirms(nadpisz);


            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujRejony(string sessionId, bool nadpisz)
        {
            ImportResult result = _ImportService.ImportAreas(nadpisz);


            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
