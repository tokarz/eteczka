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

            Assert.AreEqual(firmaZDB, znalezionaFirma);
            Assert.AreSame(firmaZDB, znalezionaFirma);

            _ConnectionFactory.Received(1).CreateConnectionToDB(_Connection);
            _ConnectionState.Received(1).ExecuteQuery(sqlQuery);
            _FirmyMapper.Received(1).MapujZSql(row);

        }
    }


   
}
