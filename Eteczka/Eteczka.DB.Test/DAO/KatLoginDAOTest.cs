using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using Eteczka.DB.Mappers;
using NSubstitute;
using NUnit.Framework;
using System.Data;
using Eteczka.DB.Utils;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatLoginDAOTest
    {
        private KatLoginDAO _Sut;

        private IKatLoginyMapper _Mapper;
        private IDbConnectionFactory _ConnectionFactory;
        private IConnectionState _ConnectionState;
        private IConnection _Connection;
        private CryptoUtils _Crypto;

        [SetUp]
        public void setUp()
        {
            _Mapper = Substitute.For<IKatLoginyMapper>();
            _ConnectionFactory = Substitute.For<IDbConnectionFactory>();
            _ConnectionState = Substitute.For<IConnectionState>();
            _Connection = Substitute.For<IConnection>();
            _Crypto = Substitute.For<CryptoUtils>();

            _Sut = new KatLoginDAO(_ConnectionFactory, _Mapper, _Connection, _Crypto);
        }

        [Test]
        public void WczytajPracownikaPoNazwieIHasle()
        {
            KatLoginy someResult = new KatLoginy();
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = 'someUser' and haslolong = 'HASHED_PASSWORD';";
            DataTable queryResult = Substitute.For<DataTable>();

            _ConnectionFactory.CreateConnectionToDB(null).ReturnsForAnyArgs(_ConnectionState);
            _Crypto.CalculateMD5Hash("somePassword").Returns("HASHED_PASSWORD");
            _ConnectionState.ExecuteQuery(sqlQuery).Returns(queryResult);
            _Mapper.Map(queryResult).Returns(someResult);
            KatLoginy result = _Sut.WczytajPracownikaPoNazwieIHasle("someUser", "somePassword");

            Assert.IsNotNull(result);
            Assert.AreEqual(someResult, result);
            _ConnectionFactory.ReceivedWithAnyArgs().CreateConnectionToDB(null);
            _ConnectionState.Received().ExecuteQuery(sqlQuery);
            _Mapper.Received().Map(queryResult);
        }


    }
}
