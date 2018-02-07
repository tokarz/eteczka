using System.Collections.Generic;
using Eteczka.Model.Entities;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;

namespace Eteczka.BE.Controllers
{
    public class KatDokumentyRodzajController : Controller
    {
        private KatDokumentyRodzajService _KatDokumentyRodzajService;

        public KatDokumentyRodzajController(KatDokumentyRodzajService KatDokumentyRodzajService)
        {
            this._KatDokumentyRodzajService = KatDokumentyRodzajService;
        }

        public ActionResult PobierzWszystkieRodzajeDokumentow(string sessionId)
        {
            List<KatDokumentyRodzaj> pobraneDokumenty = new List<KatDokumentyRodzaj>();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    pobraneDokumenty = _KatDokumentyRodzajService.PobierzRodzDok();
                }

                result = Json(new
                {
                    PobraneDokumenty = pobraneDokumenty

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;



        }

        public ActionResult DopiszRodzajDokumentu(string symbol, string nazwaDokumentu, string typEdycji, string teczkaDzial, string sessionId)
        {
            InsertResult sucess = new InsertResult();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                    sucess = _KatDokumentyRodzajService.DodajRodzajDokumentuDoBazy(symbol, nazwaDokumentu, typEdycji, teczkaDzial, sesja);
                }
                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
            
        }
        public ActionResult WylaczRodzajDokumentu(string symbol, string sessionId)
        {
            InsertResult sucess = new InsertResult();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                    sucess = _KatDokumentyRodzajService.DezaktywujRodzajDokumentu(symbol, sesja);
                }
                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public ActionResult ZnajdzRodzajDokumentuPoSymbolu(string sessionId, string symbol)
        {
            KatDokumentyRodzaj dokument = new KatDokumentyRodzaj();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    dokument = _KatDokumentyRodzajService.SzukajRodzajuDokumentuPoSymbolu(symbol);
                }
                result = Json(new
                {
                    dokument = dokument,
                    sucess = dokument != null ? true : false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}
