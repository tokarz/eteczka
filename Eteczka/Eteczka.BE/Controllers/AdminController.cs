using Eteczka.BE.DTO;
using Eteczka.BE.Model;
using Eteczka.BE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class AdminController : Controller
    {
        private IEmailService _EmailService;

        public AdminController(IEmailService emailService)
        {
            this._EmailService = emailService;
        }

        public ActionResult SendMessageToAdmin(string sessionId, AdminQuestion question)
        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                _EmailService.SendAdminQuestion(sessionId, question);
                success = true;

            }
            return Json(new
                {
                    success = success
                }, JsonRequestBehavior.AllowGet);
        }
    }

}
