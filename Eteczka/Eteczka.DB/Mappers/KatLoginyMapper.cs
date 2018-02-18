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
                DataRow row = queryResult.Rows[0];

                fetchedResult = MapSingleRow(row);
            }

            return fetchedResult;
        }

        public List<KatLoginy> MapList(DataTable queryResult)
        {
            List<KatLoginy> result = new List<KatLoginy>();
            foreach (DataRow row in queryResult.Rows)
            {
                KatLoginy singleResult = MapSingleRow(row);
                result.Add(singleResult);
            }

            return result;
        }

        public List<KatLoginyFirmy> MapFirmy(DataTable queryResult)
        {
            List<KatLoginyFirmy> result = new List<KatLoginyFirmy>();
            foreach (DataRow row in queryResult.Rows)
            {
                Uprawnienia uprawnienia = _UprawnieniaMapper.Map(row);
                KatLoginyFirmy fetchedResult = new KatLoginyFirmy()
                {
                    Identyfikator = row["identyfikator"].ToString(),
                    Firma = row["firma"].ToString(),
                    Confidential = Int32.Parse(row["confidential"].ToString()),
                    Uprawnienia = uprawnienia,
                    KodKierownik = row["kodkierownik"].ToString(),
                    Usuniety = bool.Parse(row["usuniety"].ToString())
                };

                result.Add(fetchedResult);
            }

            return result;
        }

        public List<KatLoginyDetale> MapDetails(DataTable queryResult)
        {
            List<KatLoginyDetale> result = new List<KatLoginyDetale>();
            foreach (DataRow row in queryResult.Rows)
            {
                KatLoginyDetale fetchedResult = this.MapSingleDetail(row);

                result.Add(fetchedResult);
            }

            return result;
        }

        public KatLoginyDetale MapSingleDetail(DataTable queryResult)
        {
            KatLoginyDetale result = null;
            if (queryResult != null && queryResult.Rows.Count == 1)
            {
                DataRow row = queryResult.Rows[0];

                result = this.MapSingleDetail(row);
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
        public KatLoginyDetale MapujKatLoginyDetale(AddKatLoginyDto user)
        {
            KatLoginyDetale detal = new KatLoginyDetale();
            detal.Email = user.Email;
            detal.Identyfikator = user.Identyfikator;
            detal.Imie = user.Imie;
            detal.Nazwisko = user.Nazwisko;

            return detal;
        }

        private KatLoginy MapSingleRow(DataRow row)
        {
            KatLoginy fetchedResult = new KatLoginy();
            fetchedResult.Identyfikator = row["Identyfikator".ToLower()].ToString();
            fetchedResult.Hasloshort = "(passed!)";
            fetchedResult.Haslolong = "(passed!)";

            fetchedResult.Datamodify = DateTime.Parse(row["Datamodify".ToLower()].ToString());
            fetchedResult.IsAdmin = bool.Parse(row["IsAdmin".ToLower()].ToString());
            fetchedResult.Usuniety = bool.Parse(row["Usuniety".ToLower()].ToString());

            return fetchedResult;
        }

        private KatLoginyDetale MapSingleDetail(DataRow row)
        {
            KatLoginyDetale fetchedResult = new KatLoginyDetale();
            fetchedResult.Identyfikator = row["identyfikator"].ToString();
            fetchedResult.Nazwisko = row["nazwisko"].ToString();
            fetchedResult.Imie = row["imie"].ToString();
            fetchedResult.Email = row["pocztaemail"].ToString();

            return fetchedResult;
        }
    }
}
