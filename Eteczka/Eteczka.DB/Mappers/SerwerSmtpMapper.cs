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
                fetchedSerwer.SmtpSerwer = row["smtpserwer"].ToString().Trim();
                fetchedSerwer.MailUsername = row["mailusername"].ToString().Trim();
                fetchedSerwer.MailPassword = row["mailpassword"].ToString().Trim();
                fetchedSerwer.MailSender = row["mailsender"].ToString().Trim();
                fetchedSerwer.MailSubject = row["mailsubject"].ToString().Trim();
                fetchedSerwer.MailBody = row["mailbody"].ToString().Trim();
                fetchedSerwer.PdfMasterPassword = row["pdfmasterpassword"].ToString().Trim();
                fetchedSerwer.DataModify = DateTime.Parse(row["datamodify"].ToString());
                fetchedSerwer.SmtpPort = int.Parse(row["smtpport"].ToString().Trim());
            }

            return fetchedSerwer;
        }
    }
}
