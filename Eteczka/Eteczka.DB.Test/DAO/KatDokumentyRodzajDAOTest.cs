using System.Collections.Generic;
using Eteczka.DB.Entities;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.Connection;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatDokumentyRodzajDAOTest
    {
        private KatDokumentyRodzajDAO _Sut;

        [SetUp]
        public void setUp()
        {
            ConnectionDetails connectionDetails = new ConnectionDetails("localhost", "5432", "postgres");
            DbConnectionFactory connectionFactory = new DbConnectionFactory(connectionDetails);
            _Sut = new KatDokumentyRodzajDAO(connectionFactory);
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
