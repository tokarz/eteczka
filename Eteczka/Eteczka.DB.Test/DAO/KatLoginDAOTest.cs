using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using Eteczka.DB.Mappers;
using NSubstitute;
using NUnit.Framework;
using System.Data;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatLoginDAOTest
    {
        private KatLoginDAO _Sut;

        private IKatLoginyMapper _Mapper;
        private IDbConnectionFactory _ConnectionFactory;
        private IConnectionState _ConnectionState;

        [SetUp]
        public void setUp()
        {
            _Mapper = Substitute.For<IKatLoginyMapper>();
            _ConnectionFactory = Substitute.For<IDbConnectionFactory>();
            _ConnectionState = Substitute.For<IConnectionState>();

            _Sut = new KatLoginDAO(_ConnectionFactory, _Mapper);
        }

        [Test]
        public void WczytajPracownikaPoNazwieIHasle()
        {
            KatLoginy someResult = new KatLoginy();
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = 'someUser' and haslolong = 'somePassword';";
            DataTable queryResult = Substitute.For<DataTable>();

            _ConnectionFactory.CreateConnectionToDB(null).ReturnsForAnyArgs(_ConnectionState);
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
