using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Controllers
{
    public class FilesImportController : Controller
    {
        private IImportService _ImportService;

        public FilesImportController()
        {
            _ImportService = new ImportService();
        }

        public ActionResult ImportujWszystkiePliki(bool nadpisz)
        {
            bool success = false;

            ImportResult result = _ImportService.ImportFiles(nadpisz);


            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);


        }


    }
}
