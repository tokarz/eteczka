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
    public class PracownicyController : Controller
    {
        private IPracownicyService _PracownicyService;
        private IImportService _ImportService;

        public PracownicyController(IPracownicyService pracownicyService, IImportService importService)
        {
            this._PracownicyService = pracownicyService;
            this._ImportService = importService;
        }

        public ActionResult PobierzWszystkich(string sessionId)
        {
            List<PracownikDTO> pracownicy = _PracownicyService.PobierzWszystkich();

            return Json(new
            {
                data = pracownicy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujJson(string sessionId)
        {
            bool success = false;

            success = this._ImportService.ImportujPracownikow(sessionId).ImportSukces;

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzDlaPliku(string id)
        {
            PracownikDTO pracownk = _PracownicyService.PobierzPoPeselu(id);

            return Json(new
            {
                users = pracownk
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
