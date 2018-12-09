using Eteczka.BE.Model;
using Eteczka.BE.Services;
using Eteczka.Model.DTO;
using Eteczka.Utils.Common.DTO;
using Eteczka.Utils.Logger;
using System;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class EmailController : Controller
    {
        IEadLogger LOGGER = LoggerFactory.GetLogger();

        private IEmailService _EmailService;

        public EmailController(IEmailService emailService)
        {
            this._EmailService = emailService;
        }

        [HttpPost]
        public ActionResult WyslijMailemPliki(string sessionId, DaneEmail email)
        {
            bool success = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                    success = _EmailService.WyslijPlikiMailem(sesja, email);
                }
                result = Json(new
                {
                    success
                }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception)
            {
                result = Json(new
                {
                    success = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.MAIL_SENDING, sesja, success, " ", " ", " ", "Email message " + (success ? "sent" : "not sent"));
            return result;
        }
    }
}
