using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.Connection;
using System.Data;
using Eteczka.DB.Mappers;
using System.Globalization;

namespace Eteczka.DB.DAO
{

    public class KatDokumentyRodzajDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatRodzajeDokumentowExcelMapper _KatRodzajeDokumentowExcelMapper;
        private IConnection _Connection;
        private IKatRodzajeDokumentowMapper _KatRodzajeDokumentowMapper;

        public KatDokumentyRodzajDAO(IDbConnectionFactory factory, IKatRodzajeDokumentowExcelMapper KatRodzajeDokumentowExcelMapper, IConnection connection, IKatRodzajeDokumentowMapper KatRodzajeDokumentowMapper)
        {
            this._ConnectionFactory = factory;
            this._KatRodzajeDokumentowExcelMapper = KatRodzajeDokumentowExcelMapper;
            this._Connection = connection;
            this._KatRodzajeDokumentowMapper = KatRodzajeDokumentowMapper;
        }

        public List<KatDokumentyRodzaj> PobierzWszystkieRodzDok()
        {
            string sqlQuery = "SELECT * from \"KatDokumentyRodzaj\" ORDER BY symbol";

            List<KatDokumentyRodzaj> fetchedResult = new List<KatDokumentyRodzaj>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatDokumentyRodzaj fetchedDok = _KatRodzajeDokumentowMapper.MapujZSql(row);

                fetchedResult.Add(fetchedDok);
            }

            return fetchedResult;

        }
        public bool ZapiszRodzajeDokDoBazy(string plik)
        {
            List<KatDokumentyRodzaj> RodzajeDokumentow = _KatRodzajeDokumentowExcelMapper.PobierzRodzajeDokZExcela(plik);
            StringBuilder queries = new StringBuilder();
            foreach (KatDokumentyRodzaj dokument in RodzajeDokumentow)
            {
                string values = "'" + dokument.Symbol + "', '" + dokument.Nazwa + "', 'TRUE', '22', '" + dokument.Teczkadzial + "', '" + dokument.Typedycji + "', 'Administrator', 'Administrator', '2017-09-25 22:30:00', '2017-09-25 22:30:00', '" + dokument.SystemBazowy + "', 'FALSE', '0', '"+ dokument.SymbolEad + "', 'FALSE'";
                string query = "INSERT INTO \"KatDokumentyRodzaj\" (symbol, nazwa, dokwlasny, jrwa, teczkadzial, typedycji, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety, confidential,symbolead, audyt) VALUES (" + values + ");";
                queries.Append(query);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(queries.ToString());

            return result;
        }

        public int PoliczRodzajeWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatDokumentyRodzaj\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        public bool ImportujRodzajeDokumentow(List<KatDokumentyRodzaj> rodzajeDokumentow)
        {
            StringBuilder queries = new StringBuilder();
            foreach (KatDokumentyRodzaj dokument in rodzajeDokumentow)
            {
                string values = "'" + dokument.Symbol + "', '" + dokument.Nazwa + "', 'TRUE', '22', '" + dokument.Teczkadzial + "', '" + dokument.Typedycji + "', 'Administrator', 'Administrator', '2017-09-25 22:30:00', '2017-09-25 22:30:00', '" + dokument.SystemBazowy + "', 'FALSE', '0', '" + dokument.SymbolEad + "', 'FALSE'";
                string query = "INSERT INTO \"KatDokumentyRodzaj\" (symbol, nazwa, dokwlasny, jrwa, teczkadzial, typedycji, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety, confidential,symbolead, audyt) VALUES (" + values + ");";
                queries.Append(query);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(queries.ToString());

            return result;
        }
        public bool DodajRodzajDokumentu (string symbol, string nazwaDokumentu, string typEdycji, string teczkaDzial,string idOper, string idAkcept)
        {
          
            string values = "'" + symbol + "', '" + nazwaDokumentu + "', 'TRUE', '22', '" + teczkaDzial.ToUpper() + "', '" + typEdycji.ToLower() + "', '" + idOper + "', '"+ idAkcept + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "', 'EAD', 'FALSE', '0', '" + symbol + "', 'FALSE'";
            string query = "INSERT INTO \"KatDokumentyRodzaj\" (symbol, nazwa, dokwlasny, jrwa, teczkadzial, typedycji, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety, confidential,symbolead, audyt) VALUES (" + values + ");";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(query.ToString());
            
            return result;
        }
        public KatDokumentyRodzaj ZnajdzRodzajDokumentuPoSymbolu(string symbol)
        {
            KatDokumentyRodzaj znalezionyDokument = null;
            string query = "SELECT * FROM \"KatDokumentyRodzaj\" WHERE LOWER (symbol) = '" + (symbol.ToLower().Trim()) + "' ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(query.ToString());
            try
            {
                if (result.Rows.Count>=1)
                {
                    znalezionyDokument = _KatRodzajeDokumentowMapper.MapujZSql(result.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                //logi
            }
            return znalezionyDokument;

        }
    }
}
