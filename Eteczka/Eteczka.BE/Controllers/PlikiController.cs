﻿using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using System.Configuration;
using System.IO;
using System;
using System.Linq;
using Eteczka.BE.Utils;
using Eteczka.Utils.Logger;

namespace Eteczka.BE.Controllers
{
    public class PlikiController : Controller
    {
        IEadLogger LOGGER = LoggerFactory.GetLogger();

        private IPlikiService _PlikiService;
        private PlikiUtils _PlikiUtils;

        public PlikiController(PlikiService plikiService, PlikiUtils plikiUtils)
        {
            _PlikiService = plikiService;
            _PlikiUtils = plikiUtils;
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

        public ActionResult ZmienHaslaPlikow(string sessionId, string stareHaslo, string noweHaslo)
        {
            LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.FILES_PASSWORD_CHANGE, "Password Change - START");
            bool success = false;
            StanSesji stanSesji = Sesja.PobierzStanSesji();
            if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
            {
                SessionDetails sesja = stanSesji.PobierzSesje(sessionId);

                LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.FILES_PASSWORD_CHANGE, "Password Change USER ", sesja);
                success = _PlikiService.ZmienHaslaPlikow(stareHaslo, noweHaslo);

                LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.FILES_PASSWORD_CHANGE, "Password Change USER " + (success ? " successfull " : "failed"), sesja);
            }

            return Json(new
            {
                success
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
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                SessionDetails detalseSesji = stanSesji.PobierzSesje(sessionId);
                string firma = detalseSesji.AktywnaFirma.Firma;
                string folderUzytkownika = _PlikiUtils.StworzSciezkeZListy(detalseSesji.AktywnyFolder);
                string eadRoot = ConfigurationManager.AppSettings["rootdir"];

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string filePath = Path.Combine(eadRoot, "waitingroom", firma, folderUzytkownika, postedFile.FileName);
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
            Pliki last = null;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pliki = _PlikiService.PobierzDlaUzytkownika(numeread, sesja.AktywnaFirma.Firma);
                last = pliki.Count > 0 ? pliki.OrderByDescending(f => f.DataSkanu).First() : null;
            }

            return Json(new
            {
                pliki,
                last
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
            LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.PLIK, "File Commit - START");
            bool success = false;

            StanSesji sesja = Sesja.PobierzStanSesji();

            if (sesja.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = sesja.PobierzSesje(sessionId);
                LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.PLIK, "File Commit - USER ", detaleSesji, plik.NrDokumentu);
                success = _PlikiService.ZakomitujPlikDoBazy(plik, detaleSesji.AktywnaFirma.Firma, _PlikiUtils.StworzSciezkeZListy(detaleSesji.AktywnyFolder), detaleSesji.AktywnaFirma.Identyfikator);
                LOGGER.LOG(Eteczka.Utils.Common.DTO.PoziomLogowania.INFO, Eteczka.Utils.Common.DTO.Akcja.PLIK, "File Commit - USER " + (success ? " successfull " : "failed"), detaleSesji, plik.NrDokumentu);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CzyUserWybralFolder(string sessionId)
        {
            bool success = false;
            string folder = "";
            StanSesji stanSesji = Sesja.PobierzStanSesji();


            if (stanSesji.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = stanSesji.PobierzSesje(sessionId);
                string aktywnyFolder = _PlikiUtils.StworzSciezkeZListy(detaleSesji.AktywnyFolder);
                success = (detaleSesji.AktywnyFolder != null && aktywnyFolder.Length > 0);
                folder = aktywnyFolder;
            }

            return Json(new
            {
                folder,
                success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzGitStatusDlaFirmy(string sessionId)
        {
            List<Pliki> pliki = new List<Pliki>();
            List<string> foldery = new List<string>();
            string aktywnaSciezka = "";

            StanSesji stanSesji = Sesja.PobierzStanSesji();

            if (stanSesji.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = stanSesji.PobierzSesje(sessionId);
                aktywnaSciezka = _PlikiUtils.StworzSciezkeZListy(detaleSesji.AktywnyFolder);

                pliki = _PlikiService.PobierzPlikiDlaFirmy(detaleSesji.AktywnaFirma.Firma, aktywnaSciezka);
                foldery = _PlikiService.PobierzFolderyDlaFirmy(detaleSesji.AktywnaFirma.Firma, aktywnaSciezka);
                if (detaleSesji.AktywnyFolder.Count > 0)
                {
                    foldery.Insert(0, "..");
                }
            }

            return Json(new
            {
                pliki,
                foldery,
                aktywnaSciezka
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

        public ActionResult WyslijMailemPliki(string sessionId, string adresaci, string adresaciCc, string hasloDoZip, string temat, string wiadomosc, List<string> Zalaczniki)
        {

            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                success = _PlikiService.WyslijPlikiMailem(sesja, adresaci, adresaciCc, Zalaczniki, hasloDoZip, temat, wiadomosc);
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

        public ActionResult UsunDokumentyZBazyDanych(string sessionId, List<string> ids)
        {
            ActionResult result = null;
            bool sucess = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _PlikiService.UsunDokumentyWBazie(sesja, ids);
                }
                result = Json(new
                {
                    sucess,
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

        public ActionResult ZnajdzOstatnioDodanePlikiPracownika(string sessionId, string numeread, int liczbaPlikow)
        {
            ActionResult result = null;
            List<Pliki> ZnalezionePliki = new List<Pliki>();
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

        public ActionResult PoliczDokumentyDlaPracownika(string sessionId, string numeread)
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
                    liczbaPlikow,
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

        public ActionResult PobierzStruktureZWaitingroom(string sessionId)
        {
            ActionResult result = null;
            List<string> sciezkiDoFolderow = new List<string>();
            List<string> sciezkiDoPlikow = new List<string>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    string firma = sesja.AktywnaFirma.Firma;
                    string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                    string sciezkaAktywnegoFolderu = eadRoot + "\\waitingroom\\" + firma.Trim() + "\\" + _PlikiUtils.StworzSciezkeZListy(sesja.AktywnyFolder);

                    if (Directory.Exists(sciezkaAktywnegoFolderu))
                    {
                        sciezkiDoFolderow = Directory.GetDirectories(sciezkaAktywnegoFolderu).ToList<string>();
                        sciezkiDoFolderow = _PlikiUtils.WezNazweFolderowZeSciezek(sciezkiDoFolderow);
                        if (sesja.AktywnyFolder.Count == 0)
                        {
                            sciezkiDoFolderow.Insert(0, "..");
                        }
                        sciezkiDoPlikow = Directory.GetFiles(sciezkaAktywnegoFolderu).ToList<string>();
                        sciezkiDoPlikow = _PlikiUtils.WezNazwePlikowZeSciezek(sciezkiDoPlikow);
                    }

                }
                result = Json(new
                {
                    sciezkiDoFolderow,
                    sciezkiDoPlikow,

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;

        }

        public ActionResult UstawWaitingroomDlaUsera(string sessionId, string folder)
        {
            ActionResult result = null;
            bool success = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                    if (folder == ".." && sesja.AktywnyFolder.Count() > 0)
                    {
                        sesja.AktywnyFolder.RemoveAt(sesja.AktywnyFolder.Count() - 1);
                    }
                    else if (folder != "..")
                    {
                        sesja.AktywnyFolder.Add(folder);
                    }
                    success = true;
                }
                result = Json(new
                {
                    success
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    success,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}
