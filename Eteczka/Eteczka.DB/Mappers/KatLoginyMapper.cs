using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public class KatLoginyMapper : IKatLoginyMapper
    {

        private IUprawnieniaMapper _UprawnieniaMapper;

        public KatLoginyMapper(IUprawnieniaMapper uprawnieniaMaper)
        {
            this._UprawnieniaMapper = uprawnieniaMaper;
        }

        public KatLoginy Map(DataTable queryResult)
        {
            KatLoginy fetchedResult = null;
            if (queryResult != null && queryResult.Rows.Count == 1)
            {
                fetchedResult = new KatLoginy();
                DataRow row = queryResult.Rows[0];

                fetchedResult.Identyfikator = row["Identyfikator".ToLower()].ToString();
                fetchedResult.Hasloshort = "(passed!)";
                fetchedResult.Haslolong = "(passed!)";

                fetchedResult.Datamodify = DateTime.Parse(row["Datamodify".ToLower()].ToString());
                fetchedResult.IsAdmin = bool.Parse(row["IsAdmin".ToLower()].ToString());
                fetchedResult.Usuniety = bool.Parse(row["Usuniety".ToLower()].ToString());
            }

            return fetchedResult;
        }

        public List<KatLoginyDetale> MapDetails(DataTable queryResult)
        {
            List<KatLoginyDetale> result = new List<KatLoginyDetale>();
            foreach (DataRow row in queryResult.Rows)
            {
                KatLoginyDetale fetchedResult = new KatLoginyDetale();
                fetchedResult.Identyfikator = row["identyfikator"].ToString();
                fetchedResult.Nazwisko = row["nazwisko"].ToString();
                fetchedResult.Imie = row["imie"].ToString();
                fetchedResult.Firma = row["firma"].ToString();
                fetchedResult.Email = row["pocztaemail"].ToString();
                fetchedResult.Confidential = Int32.Parse(row["confidential"].ToString());

                Uprawnienia uprawnienia = _UprawnieniaMapper.Map(row);

                fetchedResult.Uprawnienia = uprawnienia;

                result.Add(fetchedResult);
            }

            return result;
        }

        public KatLoginy MapujKatLoginy(AddKatLoginyDto user)
        {
            KatLoginy result = new KatLoginy();
            result.Datamodify = DateTime.Now;
            result.Haslolong = user.Haslolong;
            result.Hasloshort = user.Hasloshort;
            result.Identyfikator = user.Identyfikator;
            result.IsAdmin = user.IsAdmin;
            result.Usuniety = user.Usuniety;

            return result;
        }
        public List<KatLoginyDetale> MapujKatLoginyDetale(AddKatLoginyDto user)
        {
            List<KatLoginyDetale> result = new List<KatLoginyDetale>();
            foreach (KatFirmy firma in user.Firmy)
            {
                KatLoginyDetale detal = new KatLoginyDetale();
                detal.Confidential = user.Confidential;
                detal.DataModify = DateTime.Now;
                detal.Email = user.Email;
                detal.Firma = firma.Firma;
                detal.Identyfikator = user.Identyfikator;
                detal.Imie = user.Imie;
                detal.KodKierownik = user.KodKierownik;
                detal.Nazwisko = user.Nazwisko;
                detal.Uprawnienia = user.Uprawnienia;
                detal.Usuniety = user.Usuniety;

                result.Add(detal);
            }

            return result;
        }
    }
}
