using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    public class FirmyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IFirmyMapper _FirmyMapper;
        

        public FirmyDAO(IDbConnectionFactory factory, IFirmyMapper firmyMapper)
        {
            this._ConnectionFactory = factory;
            this._FirmyMapper = firmyMapper;
        }

        public bool ImportujFirmy(List<KatFirmy> firmy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatFirmy biezacyPlik in firmy)
            {
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Nazwaskrocona + "', '" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Gmina + "','" + biezacyPlik.Powiat + "', '" + biezacyPlik.Wojewodztwo + "', '" + biezacyPlik.Nip + "', '" + biezacyPlik.Regon + "', '" + biezacyPlik.Nazwa2 +
                    "', '" + biezacyPlik.Pesel + "', '" + biezacyPlik.Idoper + "', '" + biezacyPlik.Idakcept + "', '" + biezacyPlik.Nazwisko + "', '" + biezacyPlik.Imie + "', '" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatFirmy\"(firma, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, nip, regon, nazwa2, pesel, idoper, idakcept, nazwisko, imie, datamodify, dataakcept, systembazowy, usuniety) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczFirmyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatFirmy\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        public List <KatFirmy>PobierzFirmyZBazy()
        {

            List<KatFirmy> PobraneFirmy = new List<KatFirmy>();

            string sqlQuery = "SELECT * FROM \"KatFirmy\";";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatFirmy pobranaFirma = _FirmyMapper.MapujZSql(row);
                PobraneFirmy.Add(pobranaFirma);
            }

            return PobraneFirmy;
        }

        
    }
}
