using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Controllers
{
    public class PodWydzialController : Controller
    {
        private IPodWydzialService _PodWydzialService;

        public PodWydzialController(IPodWydzialService PodWydzialService)
        {
            this._PodWydzialService = PodWydzialService;
        }

        public ActionResult PobierzWszystkiePodwydzialy(string sessionId, string wydzial)
        {

            List<KatPodWydzialy> pobranePodWydzialy = new List<KatPodWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobranePodWydzialy = _PodWydzialService.PobranaListaPodWydzialow(sesja.AktywnaFirma.Firma, wydzial);
            }

            return Json(new
            {
                PodWydzialy = pobranePodWydzialy
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PobierzWszystkiePodwydzialyDoStruktury(string sessionId, string wydzial, string firma)
        {

            List<KatPodWydzialy> pobranePodWydzialy = new List<KatPodWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobranePodWydzialy = _PodWydzialService.PobranaListaPodWydzialow(firma, wydzial);
            }

            return Json(new
            {
                PodWydzialy = pobranePodWydzialy
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PobierzWszystkieAktywnePodwydzialyDoStruktury(string sessionId, string firma, string wydzial)
        {
            ActionResult result = null;
            List<KatPodWydzialy> PobranePodwydzialy = new List<KatPodWydzialy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    PobranePodwydzialy = _PodWydzialService.PobierzAktywnePodwydzialyDlaFirmy(firma, wydzial);

                    result = Json(new
                    {
                        PobranePodwydzialy,
                        sucess = PobranePodwydzialy.Count > 0 ? true : false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpGet]
        public ActionResult PobierzWszystkieNieaktywnePodwydzialyDoStruktury(string sessionId, string firma, string wydzial)
        {
            ActionResult result = null;
            List<KatPodWydzialy> PobranePodwydzialy = new List<KatPodWydzialy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    PobranePodwydzialy = _PodWydzialService.PobierzNieaktywnePodwydzialyDlaFirmy(firma, wydzial);

                    result = Json(new
                    {
                        PobranePodwydzialy,
                        sucess = PobranePodwydzialy.Count > 0 ? true : false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        public ActionResult DodajPodwydzial(string sessionId, KatPodWydzialy wydzialDoDodania)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _PodWydzialService.DodajPodWydzial(wydzialDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }
                result = Json(new
                {
                    sucess
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

        [HttpPut]
        public ActionResult EdytujPodWydzial(string sessionId, KatPodWydzialy podWydzialDoEdycji)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _PodWydzialService.EdytujPodWydzial(podWydzialDoEdycji, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }
                result = Json(new
                {
                    sucess
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

        public ActionResult UsunPodWydzial(string sessionId, KatPodWydzialy podWydzialDoUsuniecia)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _PodWydzialService.UsunPodWydzial(podWydzialDoUsuniecia, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }

                result = Json(new
                {
                    sucess
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

        [HttpPatch]
        public ActionResult PrzywrocPodwydzialWFirmie(string sessionId, KatPodWydzialy podwydzial)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult queryResult = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    queryResult = _PodWydzialService.PrzywrocPodWydzialZBazy(podwydzial, sesja.IdUzytkownika, sesja.IdUzytkownika);

                    result = Json(new
                    {
                        queryResult,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    queryResult = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }


    }
}
