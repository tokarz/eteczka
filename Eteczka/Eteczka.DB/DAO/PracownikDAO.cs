using System.Collections.Generic;
using System.Text;
using Eteczka.DB.Entities;
using System.Data;
using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;
using System;



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

        public Pracownik PobierzPracownikaPoId(string numeread)
        {

            Pracownik PobranyPracownik = null;
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE LOWER (numeread) = '" + (numeread.ToLower().Trim()) + "' ";
            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
                DataTable result = connectionState.ExecuteQuery(sqlQuery);


                if (result.Rows.Count == 1)
                {

                    PobranyPracownik = _PracownikMapper.MapujZSql(result.Rows[0]);

                }
            }

            catch (Exception ex)
            {
                
            }
             

            return PobranyPracownik;

        }


        public List<Pracownik> WyszukiwaczPracownikow(string search)
        {
            List<Pracownik> WyszukaniPracownicy = new List<Pracownik>();
            
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (imie) = '" + (search.ToLower().Trim()) + "' OR LOWER (nazwisko) = '" + (search.ToLower().Trim()) + "' OR LOWER (pesel) = '" + (search.ToLower().Trim()) + "' ";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicy.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {

            }

            return WyszukaniPracownicy;
        }

        public List<Pracownik> WyszukiwaczPracownikowPoTekscie(string search)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (nazwisko) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicyPoTekscie.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {

            }

            return WyszukaniPracownicyPoTekscie;
        }

public int PoliczPracownikowWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatPracownicy\";";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if(count != null && count.Rows != null && count.Rows.Count > 0) {
                result = int.Parse(count.Rows[0][0].ToString());
            }
            
            return result;
        }
    }
}
