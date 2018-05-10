using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Model.Entities
{
    public class SerwerSmtp
    {
        public string SmtpSerwer { get; set; }
        public string MailUsername { get; set; }
        public string MailPassword { get; set; }
        public string MailSender { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string PdfMasterPassword { get; set; }
        public DateTime DataModify { get; set; }
        public int SmtpPort { get; set; }
    }
}
