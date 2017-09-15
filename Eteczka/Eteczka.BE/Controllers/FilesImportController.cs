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
            ImportResult result = _ImportService.ImportFiles(nadpisz);

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujLokalizacjeArchiwow(string sessionId, bool nadpisz)
        {
            ImportResult result = _ImportService.ImportKatLokalPapier(nadpisz);

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

        public ActionResult ImportujMiejscaPracy(string sessionId)
        {
            ImportResult result = _ImportService.ImportWorkplaces(sessionId);

            return Json(new
       {
           success = result.ImportSukces
       }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujPodwydzialy(string sessionId)
        {
            ImportResult result = _ImportService.ImportSubDepartments(sessionId);

            return Json(new
       {
           success = result.ImportSukces
       }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportujWydzialy(string sessionId)
        {
            ImportResult result = _ImportService.ImportDepartments(sessionId);

            return Json(new
       {
           success = result.ImportSukces
       }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportujKonta5(string sessionId)
        {
            ImportResult result = _ImportService.ImportAccounts5(sessionId);

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CzyFolderIstnieje(string sessionId, string folder)
        {
            bool success = _ImportService.DoesFolderExist(firma); ;

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateSourceFolder(string sessionId, string firma)
        {
            bool success = _ImportService.CreateSourceFolder(firma); ;

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SprawdzUpdate(string sessionId, string type)
        {
            ImportResult result = _ImportService.CheckImportStatus(type);

            return Json(new
            {
                success = result.CountImportJson == result.CountImportDb,
                importJson = result.CountImportJson,
                importDb = result.CountImportDb
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WczytajDokDoPostgres()
        {
            ImportResult result = _ImportService.WczytajDokZExcela(true);
            return Json(new
            {

            }, JsonRequestBehavior.AllowGet);
        }



    }
}
