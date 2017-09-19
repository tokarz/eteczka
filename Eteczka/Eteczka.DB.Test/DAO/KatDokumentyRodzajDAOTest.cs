using System.Collections.Generic;
using Eteczka.Model.Entities;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatDokumentyRodzajDAOTest
    {
        private IKatRodzajeDokumentowExcelMapper _Mapper;
        private IConnection _Connection;
        private KatDokumentyRodzajDAO _Sut;

        [SetUp]
        public void setUp()
        {
            ConnectionDetails connectionDetails = new ConnectionDetails("postgres", "admin", "localhost", "5432", "postgres");
            DbConnectionFactory connectionFactory = new DbConnectionFactory(connectionDetails);
            _Mapper = Substitute.For<IKatRodzajeDokumentowExcelMapper>();
            _Connection = Substitute.For<IConnection>();
            _Sut = new KatDokumentyRodzajDAO(connectionFactory, _Mapper, _Connection);
        }

        [Test]
        public void GetUserByNameTest(string name)
        {
            List<KatDokumentyRodzaj> result = _Sut.PobierzWszystkich("someSessionId");

            Assert.AreEqual(2, result.Count);
        }


        public void GetAllUsers()
        {
        }

    }
}
