using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using NUnit.Framework;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatLoginDAOTest
    {
        private KatLoginDAO _Sut;

        [SetUp]
        public void setUp()
        {
            _Sut = new KatLoginDAO(new DbConnectionFactory(new ConnectionDetails("postgres", "admin", "localhost", "5432", "E-Agropin-EAD")));
        }

        [Test]
        public void WczytajPracownikaPoNazwieIHasle()
        {
            KatLoginy result = _Sut.WczytajPracownikaPoNazwieIHasle("M.Tokarz", "haslohaslo");
            Assert.IsNotNull(result);
            Assert.AreEqual("Maciej", result.Imie.Trim());
            
        }


    }
}
