using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.BE.DTO;
using Eteczka.BE.Model;
using System.Collections.Generic;

namespace Eteczka.BE.Controllers
{
    public class FilesImportController : Controller
    {
        private IImportService _ImportService;
        private IImportStateService _ImportStateService;

        public FilesImportController(IImportService importService, IImportStateService importStateService)
        {
            _ImportService = importService;
            _ImportStateService = importStateService;
        }

        public ActionResult ImportujLokalizacjeArchiwow(string sessionId, bool nadpisz)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportKatLokalPapier(nadpisz);
            }

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ImportujFirmy(string sessionId, bool nadpisz)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportFirms(nadpisz);
            }

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujRejony(string sessionId, bool nadpisz)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportAreas(nadpisz);
            }

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujMiejscaPracy(string sessionId)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportWorkplaces(sessionId);
            }

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujPodwydzialy(string sessionId)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportSubDepartments(sessionId);
            }

            return Json(new
                {
                    success = result.ImportSukces
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujWydzialy(string sessionId)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportDepartments(sessionId);
            }

            return Json(new
       {
           success = result.ImportSukces
       }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportujKonta5(string sessionId)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportService.ImportAccounts5(sessionId);
            }

            return Json(new
            {
                success = result.ImportSukces
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CzyFolderIstnieje(string sesja, string folder)
        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sesja))
            {
                success = _ImportService.DoesFolderExist(folder); ;
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateSourceFolder(string sessionId, string firma)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                success = _ImportService.CreateSourceFolder(firma); ;
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SprawdzUpdate(string sessionId, string type)
        {
            ImportResult result = CreateDefaultImportResult();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _ImportStateService.CheckImportStatus(type);
            }

            return Json(new
            {
                success = result.CountImportJson == result.CountImportDb,
                importJson = result.CountImportJson,
                importDb = result.CountImportDb
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WczytajDokDoPostgres(string sessionId)
        {
            ImportResult result = CreateDefaultImportResult();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                //result = _ImportService.WczytajDokZExcela();
                result = _ImportService.ImportKatDokumentyRodzaj(sessionId);
            }
            return Json(new
            {

            }, JsonRequestBehavior.AllowGet);
        }

        private ImportResult CreateDefaultImportResult()
        {
            ImportResult result = new ImportResult()
            {
                ImportSukces = false,
                ZaimportowanePliki = new List<string>(),
                NierozpoznanePliki = new List<string>(),
                CountImportDb = 0,
                CountImportJson = 0,
                IloscZaimportowanychPlikow = 0
            };

            return result;
        }


    }
}
