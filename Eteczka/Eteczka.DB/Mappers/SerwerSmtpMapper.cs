using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public class SerwerSmtpMapper : ISerwerSmptMapper
    {
        public SerwerSmtp MapujZSql(DataRow row)
        {
            SerwerSmtp fetchedSerwer = new SerwerSmtp();

            if (row != null)
            {
                fetchedSerwer.SmtpSerwer = row["smtpserwer"].ToString();
                fetchedSerwer.MailUsername = row["mailusername"].ToString();
                fetchedSerwer.MailPassword = row["mailpassword"].ToString();
                fetchedSerwer.MailSender = row["mailsender"].ToString();
                fetchedSerwer.MailSubject = row["mailsubject"].ToString();
                fetchedSerwer.MailBody = row["mailbody"].ToString();
                fetchedSerwer.PdfMasterPassword = row["pdfmasterpassword"].ToString();
                fetchedSerwer.DataModify = DateTime.Parse(row["datamodify"].ToString());
                fetchedSerwer.SmtpPort = int.Parse(row["smtport"].ToString());
            }

            return fetchedSerwer;
        }
    }
}
