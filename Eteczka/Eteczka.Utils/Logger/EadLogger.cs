using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using Eteczka.Utils.Common;
using Eteczka.Utils.Common.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.ComponentModel;
using Eteczka.Model;

namespace Eteczka.Utils.Logger
{
    public class EadLogger : IEadLogger
    {
        public void LOG_ZMIANY_W_TABELACH(PoziomLogowania poziom, Akcja akcja, SessionDetails sesja, bool sucess, string nazwaTabeli, object TabelaPo, object TabelaPrzed = null, string message = null)
        {
           string path = ConfigurationManager.AppSettings["rootdir"];
           string fullPath = string.Format(@"{0}/{1}/{2}.log", path, "logs", poziom.ToString());

            IToLogSerializer TabelaPrzedDoLoguBezHasel = null;

            if ( TabelaPrzed is KatLoginy || TabelaPrzed is AddKatLoginyDto)
            {
                TabelaPrzedDoLoguBezHasel = (TabelaPrzed as IToLogSerializer).WykluczPolaZHaslem(TabelaPrzed);
            }
            IToLogSerializer TabelaPoDoLoguBezHasel = null;

            if (TabelaPo is KatLoginy || TabelaPo is AddKatLoginyDto)
            {
                TabelaPoDoLoguBezHasel = (TabelaPo as IToLogSerializer).WykluczPolaZHaslem(TabelaPo);
            }

            LogTabela log = new LogTabela()
            {
                CzasWiadomosci = DateTime.Now.ToString("yyyyMMddHHmmss"),
                User = sesja.IdUzytkownika,
                Firma = sesja == null ? "" : (sesja.AktywnaFirma == null ? "" : sesja.AktywnaFirma.Firma.Trim()),
                Akcja = akcja,
                NazwaTabeli = nazwaTabeli,
                TabelaPrzed = TabelaPrzedDoLoguBezHasel != null ? this.SerializujDoJson(TabelaPrzedDoLoguBezHasel) : (TabelaPrzed != null ? this.SerializujDoJson(TabelaPrzed) :  "\"\""),
                TabelaPo = TabelaPoDoLoguBezHasel != null ? this.SerializujDoJson(TabelaPoDoLoguBezHasel) : this.SerializujDoJson(TabelaPo),
                Sucess = sucess,
                Wiadomosc = message ?? "\"\"",
                System = "EAD"
            };

            File.AppendAllText(fullPath, log.ToJsonFormat() + Environment.NewLine);

        }

        public void LOG(PoziomLogowania poziom, Akcja akcja, string widomosc, SessionDetails sesja = null, string numerEad = "")
        {
            string logLevel = ConfigurationManager.AppSettings["logLevel"];
            string path = ConfigurationManager.AppSettings["rootdir"];

            string fullPath = string.Format(@"{0}/{1}/{2}.log", path, "logs", poziom.ToString());

            Log log = new Log()
            {
                Akcja = akcja,
                Wiadomosc = widomosc,
                Firma = sesja == null ? "" : (sesja.AktywnaFirma == null ? "" : sesja.AktywnaFirma.Firma.Trim()),
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

        public void LOG_EMAIL_SENDING(EmailLog emailLog)
        {
            string rootDir = ConfigurationManager.AppSettings["rootdir"];
            string sciezkaDoPliku = Path.Combine(rootDir, "logs", "EMAILLOGS.log");

            File.AppendAllText(sciezkaDoPliku, emailLog.ToJsonFormat() + Environment.NewLine);
            
        }

        public string SerializujDoJson(object ob)
        {
            var jSetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                DateFormatString = "yyyy - MM - dd HH:mm: ss.ff"
            };
            string objectSerialized = JsonConvert.SerializeObject(ob, jSetting);

            return objectSerialized;
        }


    }
}
