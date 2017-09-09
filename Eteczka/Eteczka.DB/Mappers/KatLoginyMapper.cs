using Eteczka.DB.Entities;
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
            KatLoginy fetchedResult = new KatLoginy();
            if (queryResult != null && queryResult.Rows.Count == 1)
            {
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

        public KatLoginyDetale MapDetails(DataTable queryResult)
        {
            KatLoginyDetale fetchedResult = new KatLoginyDetale();

            if (queryResult != null && queryResult.Rows.Count == 1)
            {
                DataRow row = queryResult.Rows[0];

                fetchedResult.Identyfikator = row["Identyfikator".ToLower()].ToString();
                fetchedResult.Nazwisko = row["Nazwisko".ToLower()].ToString();
                fetchedResult.Imie = row["Imie".ToLower()].ToString();
                fetchedResult.Firma = row["Firma".ToLower()].ToString();
                fetchedResult.Email = row["pocztaemail"].ToString();

                Uprawnienia uprawnienia = _UprawnieniaMapper.Map(row);

                fetchedResult.Uprawnienia = uprawnienia;
            }




            return fetchedResult;
        }
    }
}
