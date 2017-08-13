using System.Collections.Generic;
using System.Text;
using Eteczka.DB.Entities;
using System.Data;
using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;


namespace Eteczka.DB.DAO
{
    public class PracownikDAO
    {

        private IDbConnectionFactory _ConnectionFactory;
        private IPracownikMapper _PracownikMapper;

        public PracownikDAO(IDbConnectionFactory factory, IPracownikMapper pracownikMapper)
        {
            this._ConnectionFactory = factory;
            this._PracownikMapper = pracownikMapper;
        }

        public bool ImportujPracownikow(List<Pracownik> pracownicy)
        {
            StringBuilder queries = new StringBuilder();
            foreach (Pracownik pracownik in pracownicy)
            {
                string values = "'" + pracownik.Imie + "', '" + pracownik.Nazwisko + "', '" + pracownik.PESEL + "', '" + pracownik.Numeread + "', '" + pracownik.Kraj + "', '" + pracownik.NazwiskoRodowe + "', '" + pracownik.ImieMatki + "', '" + pracownik.ImieOjca + "', '" + pracownik.PeselInny + "', '" + pracownik.IdOper + "', '" + pracownik.IdAkcept + "', '" + pracownik.DataModify + "', '" + pracownik.DataAkcept + "', '" + pracownik.DataUrodzenia + "', '" + pracownik.Imie2 + "', 'EAD', false";
                string query = "INSERT INTO \"KatPracownicy\" (imie, nazwisko, pesel, numeread, kraj, nazwiskorodowe, imiematki, imieojca, peselinny, idoper, idakcept, datamodify, dataakcept, dataurodzenia, imie2, systembazowy, usuniety) VALUES (" + values + ");";
                queries.Append(query);
            }


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            bool result = connectionState.ExecuteNonQuery(queries.ToString());


            return result;
        }

        public List<Pracownik> PobierzPracownikow()
        {

            string sqlQuery = "SELECT * from KatPracownicy;";
            List<Pracownik> fetchedUsers = new List<Pracownik>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pracownik fetchedUser = _PracownikMapper.MapujZSql(row);

                fetchedUsers.Add(fetchedUser);
            }

            return fetchedUsers;
        }

    }
}
