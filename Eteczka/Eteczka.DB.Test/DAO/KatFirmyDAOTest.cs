using Eteczka.DB.Connection;
using Eteczka.DB.DAO;
using Eteczka.DB.Mappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Tests.DAO
{
    [TestFixture]
    public class KatFirmyDAOTest
    {
        private FirmyDAO _sut;

        private IDbConnectionFactory _ConnectionFactory;
        private IFirmyMapper _FirmyMapper;
        private IConnection _Connection;
        private IConnectionState _ConnectionState;

        [SetUp]
        public void Setup()
        {
            _ConnectionFactory = Substitute.For<IDbConnectionFactory>();
            _Connection = Substitute.For<IConnection>();
            _FirmyMapper = Substitute.For<IFirmyMapper>();
            _ConnectionState = Substitute.For<IConnectionState>();

            _sut = new FirmyDAO(_ConnectionFactory, _FirmyMapper, _Connection);

        }

        [Test]
        public void PobierzFirmyZBazyTest()
        {
            List<KatFirmy> FirmyZDB = new List<KatFirmy>();
            KatFirmy firmaZDB = new KatFirmy();
            FirmyZDB.Add(firmaZDB);
            string sqlQuery = "SELECT * FROM \"KatFirmy\" ORDER BY firma";
            DataTable table = new DataTable();
            DataRow row = table.NewRow();
            table.Rows.Add(row);

            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteQuery(sqlQuery).Returns(table);
            _FirmyMapper.MapujZSql(row).Returns(firmaZDB);

            Assert.AreEqual(FirmyZDB, _sut.PobierzFirmyZBazy());

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteQuery(sqlQuery);
            _FirmyMapper.Received(1).MapujZSql(row);

        }

        [Test]
        public void DodajFirmeTest()
        {
            //Test czerwony - coś nie tak z kwerendą w teście - do poprawy.
            string idoper = "Agropin";
            string idakcept = "Agropin";
            KatFirmy firmaDododania = new KatFirmy();

            string sqlQuery = "INSERT INTO \"KatFirmy\" " +
                "(firma, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, nip, regon, nazwa2, pesel, idoper, idakcept, nazwisko, imie, datamodify, dataakcept, systembazowy, usuniety, waitingroom) " +
                "VALUES '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Agropin', 'Agropin', '', '', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', 'EAD', 'False', '';";

            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteNonQuery(sqlQuery).Returns(true);

            Assert.IsTrue(_sut.DodajFirme(firmaDododania, idoper, idakcept));

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteNonQuery(sqlQuery);
            
        }
        [Test]
        public void PoliczFirmyWBazieTest()
        {
            string sqlQuery = "SELECT COUNT(*) FROM \"KatFirmy\"; ";

            DataTable table = new DataTable();
            table.Columns.Add("NewColumn", typeof(System.Int32));
            DataRow row = table.NewRow();
            row["NewColumn"] = 4;
            
            table.Rows.Add(row);
            
            
            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteQuery(sqlQuery).Returns(table);

            Assert.AreEqual(4, _sut.PoliczFirmyWBazie());

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteQuery(sqlQuery);

        }

        [Test]
        public void WyszukajFirmePoNipieTest()
        {

            KatFirmy firmaZDB = new KatFirmy();
            

            string nip = "0123456789";
            string sqlQuery = "SELECT * FROM \"KatFirmy\" WHERE nip = '" + nip + "'";

            DataTable table = new DataTable();

            DataRow row = table.NewRow();
            table.Rows.Add(row);
            

            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteQuery(sqlQuery).Returns(table);
            _FirmyMapper.MapujZSql(row).Returns(firmaZDB);

            KatFirmy znalezionaFirma = _sut.WyszukajFirmePoNipie("0123456789");

            Assert.IsNotNull(znalezionaFirma);
            Assert.AreEqual(firmaZDB, znalezionaFirma);

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteQuery(sqlQuery);
            _FirmyMapper.Received(1).MapujZSql(row);

        }
        [Test]
        public void PrzywrocFirmeZBazyTest()
        {
            string sqlQuery = "UPDATE \"KatFirmy\" SET usuniety = 'false' WHERE nip = '0123456789'";

            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteNonQuery(sqlQuery).Returns(true);

            Assert.IsTrue(_sut.PrzywrocFirmeZBazy("0123456789"));

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteNonQuery(sqlQuery);

        }
        [Test]
        public void DezaktywujFirmeTest()
        {

            string sqlQuery = "UPDATE \"KatFirmy\" SET usuniety = 'true', idoper = 'Agropin', idakcept = 'Agropin', " +
             "datamodify = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', dataakcept = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "'  WHERE nip = '0123456789'";
            
            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteNonQuery(sqlQuery).Returns(true);

            Assert.IsTrue(_sut.DezaktywujFirme("0123456789", "Agropin", "Agropin"));

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteNonQuery(sqlQuery);
        }
        [Test]
        public void EdytujFirmeTest()
        {
            KatFirmy firmaDoEdycji = new KatFirmy();
            string sqlUpdateQuery = "UPDATE \"KatFirmy\" SET  nazwa = '', nazwaskrocona = '', ulica = '', numerdomu = '', numerlokalu = '', miasto = '', kodpocztowy = '', poczta = '', gmina = '', powiat = '', wojewodztwo = '', nip = '', regon = '', nazwa2 = '', pesel = '', idoper = 'Agropin', idakcept = 'Agropin', nazwisko = '', imie = '', datamodify = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', dataakcept = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "', systembazowy = 'EAD', usuniety = 'False', waitingroom = '' WHERE nip = '0123456789';";
            
            _ConnectionFactory.CreateConnectionToDB(_Connection).Returns(_ConnectionState);
            _ConnectionState.ExecuteNonQuery(sqlUpdateQuery).Returns(true);

            Assert.IsTrue(_sut.EdytujFirme(firmaDoEdycji, "0123456789", "Agropin", "Agropin"));

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteNonQuery(sqlUpdateQuery);

        }

    }


   
}
