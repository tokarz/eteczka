using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    public class FirmyDAO : IFirmyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IFirmyMapper _FirmyMapper;
        private IConnection _Connection;

        public FirmyDAO(IDbConnectionFactory factory, IFirmyMapper firmyMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._FirmyMapper = firmyMapper;
            this._Connection = connection ;
        }

        public bool ImportujFirmy(List<KatFirmy> firmy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            List<KatFirmy> juzDodaneFirmy = PobierzFirmyZBazy();

            var jeszczeNieDodaneFirmy = firmy.Where(firmaDoDodania => {
                return !juzDodaneFirmy.Any(istniejacaFirma => istniejacaFirma.Nip == firmaDoDodania.Nip);
            });

            foreach (KatFirmy biezacyPlik in jeszczeNieDodaneFirmy)
            {
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Nazwaskrocona + "', '" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Gmina + "','" + biezacyPlik.Powiat + "', '" + biezacyPlik.Wojewodztwo + "', '" + biezacyPlik.Nip + "', '" + biezacyPlik.Regon + "', '" + biezacyPlik.Nazwa2 +
                    "', '" + biezacyPlik.Pesel + "', '" + biezacyPlik.Idoper + "', '" + biezacyPlik.Idakcept + "', '" + biezacyPlik.Nazwisko + "', '" + biezacyPlik.Imie + "', '" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatFirmy\"(firma, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, nip, regon, nazwa2, pesel, idoper, idakcept, nazwisko, imie, datamodify, dataakcept, systembazowy, usuniety) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczFirmyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatFirmy\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        public List<KatFirmy> PobierzFirmyZBazy(string orderBy = "firma")
        {
            List<KatFirmy> PobraneFirmy = new List<KatFirmy>();

            string sqlQuery = "SELECT * FROM \"KatFirmy\" ORDER BY " + orderBy;

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatFirmy pobranaFirma = _FirmyMapper.MapujZSql(row);
                PobraneFirmy.Add(pobranaFirma);
            }

            return PobraneFirmy;
        }

        public bool ZapiszKatalogRoboczy(string firma, string sciezka)
        {
            bool result = false;

            string sqlQuery = "UPDATE \"KatFirmy\" SET  waitingroom='" + sciezka + "' WHERE firma = '" + firma + "';";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool DodajFirme(KatFirmy firmaDoDodania, string idoper, string idakcept)
        {
            bool result = false;
            object[] objects = new object[]
            {
                firmaDoDodania.Firma,
                firmaDoDodania.Nazwa,
                firmaDoDodania.Nazwaskrocona,
                firmaDoDodania.Ulica,
                firmaDoDodania.Numerdomu,
                firmaDoDodania.Numerlokalu,
                firmaDoDodania.Miasto,
                firmaDoDodania.Kodpocztowy,
                firmaDoDodania.Poczta,
                firmaDoDodania.Gmina,
                firmaDoDodania.Powiat,
                firmaDoDodania.Wojewodztwo,
                firmaDoDodania.Nip,
                firmaDoDodania.Regon,
                firmaDoDodania.Nazwa2,
                firmaDoDodania.Pesel,
                idoper,
                idakcept,
                firmaDoDodania.Nazwisko,
                firmaDoDodania.Imie,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                "EAD",
                false,
                firmaDoDodania.Waitingroom

    };
            string values = string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}', '{24}');", objects);
            string sqlQuery = "INSERT INTO \"KatFirmy\" (firma, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, nip, regon, nazwa2, pesel, idoper, idakcept, nazwisko, imie, datamodify, dataakcept, systembazowy, usuniety, waitingroom) VALUES" + values;

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool EdytujFirme(KatFirmy firmaDoEdycji, string nipPrzedZmiana,  string idoper, string idakcept)
        {
            bool result = false;
            object[] objects = new object[]
            {
                firmaDoEdycji.Firma,
                firmaDoEdycji.Nazwa,
                firmaDoEdycji.Nazwaskrocona,
                firmaDoEdycji.Ulica,
                firmaDoEdycji.Numerdomu,
                firmaDoEdycji.Numerlokalu,
                firmaDoEdycji.Miasto,
                firmaDoEdycji.Kodpocztowy,
                firmaDoEdycji.Poczta,
                firmaDoEdycji.Gmina,
                firmaDoEdycji.Powiat,
                firmaDoEdycji.Wojewodztwo,
                firmaDoEdycji.Nip,
                firmaDoEdycji.Regon,
                firmaDoEdycji.Nazwa2,
                firmaDoEdycji.Pesel,
                idoper,
                idakcept,
                firmaDoEdycji.Nazwisko,
                firmaDoEdycji.Imie,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                "EAD",
                false,
                firmaDoEdycji.Waitingroom

    };
            string sqlUpdateQuery = string.Format("UPDATE \"KatFirmy\" SET  nazwa = '{1}', nazwaskrocona = '{2}', ulica = '{3}', numerdomu = '{4}', numerlokalu = '{5}', miasto = '{6}', kodpocztowy = '{7}', poczta = '{8}', gmina = '{9}', powiat = '{10}', wojewodztwo = '{11}', nip = '{12}', regon = '{13}', nazwa2 = '{14}', pesel = '{15}', idoper = '{16}', idakcept = '{17}', nazwisko = '{18}', imie = '{19}', datamodify = '{20}', dataakcept = '{21}', systembazowy = '{22}', usuniety = '{23}', waitingroom = '{24}' WHERE nip = '" + nipPrzedZmiana + "';", objects);


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlUpdateQuery);

            return result;
        }

        public bool DezaktywujFirme(string nip, string idoper, string idakcept)
        {
            bool result = false;
            string sqlQuery = "UPDATE \"KatFirmy\" SET usuniety = 'true', idoper = '" + idoper + "', idakcept = '" + idakcept + "', " +
                "datamodify = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', dataakcept = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "'  WHERE nip = '" + nip + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;   
        }

        public bool PrzywrocFirmeZBazy(string nip)
        {

            bool result = false;

            string sqlQuery = "UPDATE \"KatFirmy\" SET usuniety = 'false' WHERE nip = '" + nip + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public KatFirmy PobierzFirmePoNipie(string nip)
        {
            KatFirmy znalezionaFirma = null;

            string sqlQuery = "SELECT * FROM \"KatFirmy\" WHERE nip = '" + nip + "'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null && table.Rows != null && table.Rows.Count ==1)
            {
                znalezionaFirma = _FirmyMapper.MapujZSql(table.Rows[0]);
            }
            return znalezionaFirma;
        }

        public List<KatFirmy> WyszukajFirmePoNazwieNipieLubFirmie(string search)
        {
            List<KatFirmy> result = new List<KatFirmy>();

            string sqlQuery = "SELECT * FROM \"KatFirmy\" WHERE UPPER(nip) || UPPER(nazwa) || UPPER(firma) LIKE UPPER('%" + search + "%');";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            
                foreach (DataRow row in table.Rows)
                {
                   result.Add( _FirmyMapper.MapujZSql(row));
                }

            return result;
        }





    }
}
