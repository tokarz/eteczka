using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.Connection;
using System.Data;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{

    public class KatDokumentyRodzajDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatRodzajeDokumentowExcelMapper _KatRodzajeDokumentowExcelMapper;
        private IConnection _Connection;

        public KatDokumentyRodzajDAO(IDbConnectionFactory factory, IKatRodzajeDokumentowExcelMapper KatRodzajeDokumentowExcelMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._KatRodzajeDokumentowExcelMapper = KatRodzajeDokumentowExcelMapper;
            this._Connection = connection;
        }

        public List<KatDokumentyRodzaj> PobierzWszystkich(string sessionId)
        {
            string sqlQuery = "SELECT * from KatDokumentyRodzaj";

            List<KatDokumentyRodzaj> fetchedResult = new List<KatDokumentyRodzaj>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatDokumentyRodzaj fetchedDok = new KatDokumentyRodzaj();
                fetchedDok.Symbol = row[0].ToString();
                fetchedDok.Nazwa = row[1].ToString();
                fetchedDok.Dokwlasny = bool.Parse(row[2].ToString());
                fetchedDok.Jrwa = row[3].ToString();
                fetchedDok.Teczkadzial = row[4].ToString();
                fetchedDok.Typedycji = row[5].ToString();
                fetchedDok.Idoper = row[6].ToString();
                fetchedDok.Idakcept = row[7].ToString();
                fetchedDok.Datamodify = DateTime.Parse(row[8].ToString());
                fetchedDok.Dataakcept = DateTime.Parse(row[9].ToString());

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
                string values = "'" + dokument.Symbol + "', '" + dokument.Nazwa + "', '" + dokument.Teczkadzial + "', '" + dokument.Typedycji + "', '" + dokument.SystemBazowy + "'";
                string query = "INSERT INTO \"KatDokumentyRodzaj\" ( symbol, nazwa, teczkadzial, typedycji,systembazowy) VALUES (" + values + ");";
                queries.Append(query);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(queries.ToString());

            return result;
        }
    }
}
