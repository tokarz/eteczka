using Eteczka.BE.DTO;
using Eteczka.BE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public class EmailService : IEmailService
    {
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
    }
}
