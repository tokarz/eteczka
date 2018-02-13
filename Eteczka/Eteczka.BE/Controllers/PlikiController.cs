using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using System.Configuration;
using System.IO;
using System;

namespace Eteczka.BE.Controllers
{
    public class PlikiController : Controller
    {
        private IPlikiService _PlikiService;

        public PlikiController(PlikiService plikiService)
        {
            _PlikiService = plikiService;
        }

        public ActionResult PobierzWszystkie(string sessionId)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pliki = _PlikiService.PobierzWszystkie();
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzMetadane(string sessionId, string plik)
        {
            MetaDanePliku meta = new MetaDanePliku(); ;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                meta = _PlikiService.PobierzMetadane(plik);
            }

            return Json(new
            {
                meta
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DodajDoWaitingRoomu(string sessionId)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                var httpRequest = HttpContext.Request;
                string firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma;
                string eadRootName = ConfigurationManager.AppSettings["rootdir"];
                string eadRoot = Environment.GetEnvironmentVariable(eadRootName);

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string filePath = Path.Combine(eadRoot, "waitingroom", firma, postedFile.FileName);
                        postedFile.SaveAs(filePath);
                    }
                }

            }

            return Json(new
            {
                success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzDlaUzytkownika(string sessionId, string numeread)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pliki = _PlikiService.PobierzDlaUzytkownika(numeread, sesja.AktywnaFirma);
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzStanZeskanowanychPlikow(string sessionId)
        {
            StanPlikow stanPlikow = null;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                stanPlikow = _PlikiService.PobierzStanPlikow(sessionId);
            }

            return Json(new
            {
                stan = stanPlikow
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UsunPliki(string sessionId, List<Pliki> pliki)
        {
            bool success = false;

            StanSesji sesja = Sesja.PobierzStanSesji();

            if (sesja.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = sesja.PobierzSesje(sessionId);
                //Usun pliki
                //success = _PlikiService.(plik, detaleSesji.AktywnaFirma, detaleSesji.AktywnyUser.Identyfikator);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult KomitujPlik(string sessionId, KomitPliku plik)
        {
            bool success = false;

            StanSesji sesja = Sesja.PobierzStanSesji();

            if (sesja.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = sesja.PobierzSesje(sessionId);
                success = _PlikiService.ZakomitujPlikDoBazy(plik, detaleSesji.AktywnaFirma, detaleSesji.AktywnyUser.Identyfikator);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzGitStatusDlaFirmy(string sessionId, string firma)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pliki = _PlikiService.PobierzPlikiDlaFirmy(firma);
            }

            return Json(new
            {
                newfiles = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PobierzPlikiWgFiltrow(string sessionId, FiltryPlikow filtry)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                pliki = _PlikiService.SzukajPlikiZFiltrow(sesja, filtry);
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyslijMailemPliki(string sessionId, string adresaci,  string hasloDoZip, string temat, string wiadomosc, List<string> Zalaczniki)
        {

            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                success = _PlikiService.WyslijPlikiMailem(sesja, adresaci, Zalaczniki, hasloDoZip, temat, wiadomosc);
            }
            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult EdytujDokumentZBazy(string sessionId, string idPliku, KomitPliku plik)
        {

            bool sucess = false;
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _PlikiService.EdytujDokumentWBazie(sesja, plik, idPliku);
                }
                result = Json(new
                {
                    sucess = sucess
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                });
            }
            return result;
        }

        public ActionResult ZnajdzOstatnioDodanePlikiPracownika(string sessionId, string numeread, int liczbaPlikow)
        {
            ActionResult result = null;
            List<Pliki> ZnalezionePliki = new List <Pliki>();
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    ZnalezionePliki = _PlikiService.SzukajOstatnioDodanePlikiPrac(sesja, numeread, liczbaPlikow);
                }
                result = Json(new
                {
                    sucess = ZnalezionePliki != null && ZnalezionePliki.Count > 0 ? true : false,
                    pliki = ZnalezionePliki
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

        public ActionResult PoliczDokumentyDlaPracownika (string sessionId, string numeread)
        {
            int liczbaPlikow = 0;
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    liczbaPlikow = _PlikiService.ZliczPlikiWTeczcePracownika(sesja, numeread);
                }
                result = Json(new
                {
                    liczbaPlikow = liczbaPlikow,
                    sucess = true
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
