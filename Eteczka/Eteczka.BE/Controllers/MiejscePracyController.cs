using System.Web.Mvc;
using System.Collections.Generic;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;
using System;

namespace Eteczka.BE.Controllers
{
    public class MiejscePracyController : Controller
    {
        private IMiejscePracyService _MiejscePracyService;

        public MiejscePracyController(IMiejscePracyService miejscePracyService)
        {
            this._MiejscePracyService = miejscePracyService;
        }

        public ActionResult MiejscePracyDlaPracownika(string sessionId, string numeread)
        {
            List<MiejscePracyDlaPracownika> miejscaPracy = new List<MiejscePracyDlaPracownika>();
            SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

            if (sesja != null)
            {
                if (numeread != null)
                {
                    miejscaPracy = _MiejscePracyService.PobierzMiejscaPracyDlaPracownika(numeread, sesja.AktywnaFirma.Firma);
                }
            }
            return Json(new
            {
                MiejscaPracy = miejscaPracy
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DodajMiejscePracy(string sessionId, MiejscePracy miejsceDoDodania)
        {
            ActionResult result = null;
            InsertResult wynikInserta = null;
            SessionDetails detaleSesji = null;
            bool hasPermissions = false;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    detaleSesji = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = detaleSesji.AktywnaFirma.Uprawnienia.RolaAddPracownik ? true : false;

                    if (hasPermissions)
                    {
                        wynikInserta = _MiejscePracyService.DodajMiejscePracy(detaleSesji, miejsceDoDodania);
                        result = Json(new
                        {
                            sucess = wynikInserta
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
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

        [HttpPut]
        public ActionResult EdytujMiejscePracy(string sessionId, MiejscePracy miejsceDoEdycji)
        {
            ActionResult result = null;
            SessionDetails detaleSesji = null;
            InsertResult wynikInserta = null;
            bool hasPermissions = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    detaleSesji = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = detaleSesji.AktywnaFirma.Uprawnienia.RolaModifyPracownik ? true : false;

                    if (hasPermissions)
                    {
                        wynikInserta = _MiejscePracyService.EdytujMiejscePracy(detaleSesji, miejsceDoEdycji);
                        result = Json(new
                        {
                            sucess = wynikInserta
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
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
        public ActionResult UsunMiejscePracy(string sessionId, MiejscePracy miejsceDoUsuniecia)
        {
            ActionResult result = null;
            InsertResult wynikUpdate = null;
            SessionDetails detaleSesji = null;
            bool hasPermissions = false;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    detaleSesji = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = detaleSesji.AktywnaFirma.Uprawnienia.RolaModifyPracownik ? true : false;

                    if (hasPermissions)
                    {
                        wynikUpdate = _MiejscePracyService.UsunMiejscePracy(detaleSesji, miejsceDoUsuniecia);
                        result = Json(new
                        {
                            sucess = wynikUpdate
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
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
