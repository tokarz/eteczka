using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.DB.Mappers;
using Eteczka.DB.Utils;
using Eteczka.Model.DTO;

namespace Eteczka.DB.DAO
{
    public class KatLoginDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatLoginyMapper _KatLoginyMapper;
        private IConnection _Connection;
        private CryptoUtils _Crypto;

        public KatLoginDAO(IDbConnectionFactory factory, IKatLoginyMapper mapper, IConnection connection, CryptoUtils crypto)
        {
            this._ConnectionFactory = factory;
            this._KatLoginyMapper = mapper;
            this._Connection = connection;
            this._Crypto = crypto;
        }

        public bool UsunFirmeUzytkownika(KatLoginyFirmy firma)
        {
            string sqlQuery = "UPDATE \"KatLoginyFirmy\" SET usuniety=true WHERE identyfikator='" + firma.Identyfikator.Trim() + "' and firma='" + firma.Firma + "';";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public List<KatLoginy> WczytajWszystkichUzytkownikow()
        {
            string sqlQuery = "SELECT * from \"KatLoginy\";";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginy> fetchedResult = this._KatLoginyMapper.MapList(queryResult);

            return fetchedResult;
        }

        public KatLoginy WczytajPracownikaPoNazwieIHasle(string username, string password)
        {
            //SQL Injection Threat!
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = '" + username + "' and haslolong = '" + _Crypto.CalculateMD5Hash(password) + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            KatLoginy fetchedResult = this._KatLoginyMapper.Map(queryResult);


            return fetchedResult;
        }

        public bool DodajNowegoPracownika(KatLoginy pracownik, KatLoginyDetale detal)
        {
            bool result = false;

            if (pracownik.Haslolong != null)
            {
                pracownik.Haslolong = _Crypto.CalculateMD5Hash(pracownik.Haslolong);
            }

            if (pracownik.Hasloshort != null)
            {
                pracownik.Hasloshort = _Crypto.CalculateMD5Hash(pracownik.Hasloshort);
            }

            StringBuilder sqlBatch = new StringBuilder();
            string katLoginyWartosci = string.Format("'{0}','{1}','{2}','{3}',{4},{5}", pracownik.Identyfikator, pracownik.Hasloshort, pracownik.Haslolong, pracownik.Datamodify, pracownik.IsAdmin, pracownik.Usuniety);
            string dodajUzytkownika = "INSERT INTO \"KatLoginy\" (identyfikator, hasloshort, haslolong, datamodify, isadmin, usuniety) VALUES (" + katLoginyWartosci + ");";
            sqlBatch.Append(dodajUzytkownika);

            string detaleWartosci = string.Format("'{0}','{1}','{2}','{3}'",
                detal.Identyfikator,
                detal.Nazwisko,
                detal.Imie,
                detal.Email);

            string katLoginyDetal = "INSERT INTO \"KatLoginyDetale\" (identyfikator, nazwisko, imie, pocztaemail) VALUES (" + detaleWartosci + ");";

            sqlBatch.Append(katLoginyDetal);

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(sqlBatch.ToString());

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool ZmienHasloUzytkownika(AddKatLoginyDto userDoZmianyHasla)
        {
            bool result = false;

            if (userDoZmianyHasla.Haslolong != null)
            {
                userDoZmianyHasla.Haslolong = _Crypto.CalculateMD5Hash(userDoZmianyHasla.Haslolong);

                string updateString = "UPDATE \"KatLoginy\" SET haslolong='" + userDoZmianyHasla.Haslolong + "' WHERE identyfikator = '" + userDoZmianyHasla.Identyfikator + "';";

                try
                {
                    IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                    result = connectionState.ExecuteNonQuery(updateString.ToString());

                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }
        public bool UsunUzytkownika(string id)
        {
            bool result = false;
            string updateString = "UPDATE \"KatLoginy\" SET usuniety = true WHERE identyfikator = '" + id + "';";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(updateString.ToString());
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public KatLoginyDetale WczytajDetaleDlaUzytkownika(string id)
        {
            string sqlQuery = "SELECT * from \"KatLoginyDetale\" WHERE identyfikator = '" + id + "';";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            KatLoginyDetale fetchedResult = this._KatLoginyMapper.MapSingleDetail(queryResult);

            return fetchedResult;
        }

        

            public bool AktualizujFirmeDlaUzytkownika(KatLoginyFirmy firma)
        {
            bool result = false;
            string updateFirmy = string.Format("identyfikator='{0}',firma='{1}',rolareadonly='{2}',rolaaddpracownik='{3}',rolamodifypracownik='{4}', rolaaddfile='{5}', rolamodifyfile='{6}',rolaslowniki='{7}',rolasendmail='{8}',rolaraport='{9}',rolaraportexport='{10}', roladoubleakcept='{11}', datamodify='{12}',usuniety='{13}',confidential='{14}', kodkierownik='{15}'", firma.Identyfikator.Trim(), firma.Firma.Trim(), firma.Uprawnienia.RolaReadOnly, firma.Uprawnienia.RolaAddPracownik, firma.Uprawnienia.RolaModifyPracownik, firma.Uprawnienia.RolaAddFile, firma.Uprawnienia.RolaModifyFile, firma.Uprawnienia.RolaSlowniki, firma.Uprawnienia.RolaSendEmail, firma.Uprawnienia.RolaRaport, firma.Uprawnienia.RolaRaportExport, firma.Uprawnienia.RolaDoubleAkcept, firma.DataModify, firma.Usuniety, firma.Confidential, firma.KodKierownik);
            string dodajUzytkownika = "UPDATE public.\"KatLoginyFirmy\" SET " + updateFirmy + "  WHERE identyfikator='" + firma.Identyfikator + "' AND firma='" + firma.Firma + "';";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(dodajUzytkownika);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool DodajFirmeDlaUzytkownika(KatLoginyFirmy firma)
        {
            bool result = false;
            string nowaFirma = string.Format("'{0}','{1}','{2}','{3}','{4}', '{5}', '{6}','{7}','{8}','{9}','{10}', '{11}', '{12}','{13}','{14}', '{15}'", firma.Identyfikator.Trim(), firma.Firma.Trim(), firma.Uprawnienia.RolaReadOnly, firma.Uprawnienia.RolaAddPracownik, firma.Uprawnienia.RolaModifyPracownik, firma.Uprawnienia.RolaAddFile, firma.Uprawnienia.RolaModifyFile, firma.Uprawnienia.RolaSlowniki, firma.Uprawnienia.RolaSendEmail, firma.Uprawnienia.RolaRaport, firma.Uprawnienia.RolaRaportExport, firma.Uprawnienia.RolaDoubleAkcept, firma.DataModify, firma.Usuniety, firma.Confidential, firma.KodKierownik);
            string dodajUzytkownika = "INSERT INTO public.\"KatLoginyFirmy\" (identyfikator, firma, rolareadonly, rolaaddpracownik, rolamodifypracownik,rolaaddfile, rolamodifyfile, rolaslowniki, rolasendmail, rolaraport,rolaraportexport, roladoubleakcept, datamodify, usuniety, confidential, kodkierownik) VALUES (" + nowaFirma + ");";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(dodajUzytkownika);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public List<KatLoginyFirmy> WczytajWszystkieFirmy()
        {
            string sqlQuery = "SELECT * from \"KatLoginyFirmy\" WHERE usuniety = false;";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyFirmy> fetchedResult = this._KatLoginyMapper.MapFirmy(queryResult);

            return fetchedResult;
        }

        public List<KatLoginyFirmy> WczytajFirmyDlaUzytkownika(string id)
        {
            string sqlQuery = "SELECT * from \"KatLoginyFirmy\" WHERE identyfikator = '" + id + "' and usuniety = false;";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyFirmy> fetchedResult = this._KatLoginyMapper.MapFirmy(queryResult);

            return fetchedResult;
        }



        public List<KatLoginyDetale> WczytajWszystkieDetale(bool czyAdminTez = false)
        {
            string wherePart = ";";
            if (!czyAdminTez)
            {
                wherePart = " WHERE identyfikator != 'Administrator';";
            }
            string sqlQuery = "SELECT * from \"KatLoginyDetale\"" + wherePart;


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyDetale> fetchedResult = this._KatLoginyMapper.MapDetails(queryResult);

            return fetchedResult;
        }
    }
}
