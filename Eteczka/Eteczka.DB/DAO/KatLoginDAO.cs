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

        public bool UsunFirmeUzytkownika(KatLoginy user, string firma)
        {
            string sqlQuery = "UPDATE \"KatLoginyDetale\" SET usuniety=true WHERE identyfikator='" + user.Identyfikator.Trim() + "' and firma='" + firma + "';";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
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

        public bool DodajNowegoPracownika(KatLoginy pracownik, List<KatLoginyDetale> detale)
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
            foreach (KatLoginyDetale detal in detale)
            {
                if (detal.Uprawnienia == null)
                {
                    detal.Uprawnienia = new Uprawnienia();
                    detal.Uprawnienia.RolaAddFile = true;
                    detal.Uprawnienia.RolaAddPracownik = true;
                    detal.Uprawnienia.RolaDoubleAkcept = true;
                    detal.Uprawnienia.RolaModifyFile = true;
                    detal.Uprawnienia.RolaModifyPracownik = true;
                    detal.Uprawnienia.RolaRaport = true;
                    detal.Uprawnienia.RolaRaportExport = true;
                    detal.Uprawnienia.RolaReadOnly = true;
                    detal.Uprawnienia.RolaSendEmail = true;
                    detal.Uprawnienia.RolaSlowniki = true;
                }

                string detaleWartosci = string.Format("'{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},'{15}',{16},{17},'{18}'",
                    detal.Identyfikator,
                    detal.Nazwisko,
                    detal.Imie,
                    detal.Firma,
                    detal.Email,
                    detal.Uprawnienia.RolaReadOnly,
                    detal.Uprawnienia.RolaAddPracownik,
                    detal.Uprawnienia.RolaModifyPracownik,
                    detal.Uprawnienia.RolaAddFile,
                    detal.Uprawnienia.RolaModifyFile,
                    detal.Uprawnienia.RolaSlowniki,
                    detal.Uprawnienia.RolaSendEmail,
                    detal.Uprawnienia.RolaRaport,
                    detal.Uprawnienia.RolaRaportExport,
                    detal.Uprawnienia.RolaDoubleAkcept,
                    detal.DataModify,
                    detal.Usuniety,
                    detal.Confidential,
                    detal.KodKierownik);
                string katLoginyDetal = "INSERT INTO \"KatLoginyDetale\" (identyfikator, nazwisko, imie, firma, pocztaemail, rolareadonly, rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile, rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept, datamodify, usuniety, confidential, kodkierownik) VALUES (" + detaleWartosci + ");";

                sqlBatch.Append(katLoginyDetal);
            }

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
        public bool UsunUzytkownika(AddKatLoginyDto user)
        {
            bool result = false;
            string updateString = "UPDATE \"KatLoginy\" SET usuniety = true WHERE identyfikator = '" + user.Identyfikator + "';";

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

        public List<KatLoginyDetale> WczytajDetaleDlaUzytkownika(string id)
        {
            string sqlQuery = "SELECT * from \"KatLoginyDetale\" WHERE identyfikator = '" + id + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyDetale> fetchedResult = this._KatLoginyMapper.MapDetails(queryResult);


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
