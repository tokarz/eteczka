using Eteczka.BE.DTO;
using Eteczka.BE.Model;
using Eteczka.BE.Utils;
using Eteczka.Model.DTO;
using System.Configuration;
using System.Net.Mail;

namespace Eteczka.BE.Services
{
    public class EmailService : IEmailService
    {
        private PlikiUtils _PlikiUtils;

        public EmailService(PlikiUtils plikiUtils)
        {
            this._PlikiUtils = plikiUtils;
        }

        public void SendAdminQuestion(string sessionId, AdminQuestion email)
        {
            MailMessage mail = new MailMessage("eteczka@eteczka.info", ConfigurationManager.AppSettings["adminemail"]);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings["smtpserver"];
            mail.Subject = email.Topic;
            mail.Body = email.Username + ", " + email.Password + " " + email.Remarks;
            client.Send(mail);
        }

        public bool WyslijPlikiMailem(SessionDetails sesja, DaneEmail email)
        {
            bool result = _PlikiUtils.WyslijPlikiMailem(sesja.AktywnaFirma.Firma, sesja.IdUzytkownika, email);

            return result;
        }
    }
}
