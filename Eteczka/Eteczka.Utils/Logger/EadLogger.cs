using Eteczka.BE.Model;
using Eteczka.Utils.Common.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Utils.Logger
{
    public class EadLogger : IEadLogger
    {
        public void LOG(PoziomLogowania poziom, Akcja akcja, string widomosc, SessionDetails sesja = null, string numerEad = "")
        {
            string logLevel = ConfigurationManager.AppSettings["logLevel"];
            string path = ConfigurationManager.AppSettings["rootdir"];

            string fullPath = string.Format(@"{0}/{1}/{2}.log", path, "logs", poziom.ToString());

            Log log = new Log()
            {
                Akcja = akcja,
                Wiadomosc = widomosc,
                Firma = sesja == null ? "" : sesja.AktywnaFirma.Firma.Trim(),
                CzasWiadomosci = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Id = "ToBeGenerated",
                InformacjeDodatkowe = "",
                NumerEad = numerEad,
                UserId = sesja == null ? "" : sesja.IdUzytkownika.Trim()
            };

            if (poziom == PoziomLogowania.DEBUG)
            {
                if (logLevel == "debug")
                {
                    File.AppendAllText(fullPath, "{" + log.ToString() + "};" + Environment.NewLine);
                }
            }
            else
            {
                File.AppendAllText(fullPath, "{" + log.ToString() + "};" + Environment.NewLine);
            }
        }
    }
}
