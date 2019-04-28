using Eteczka.BE.Model;
using Eteczka.BE.Services;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Be.Tests.Services
{
    [TestFixture]
    public class RejonyServiceTest
    {
        private IRejonyService _Sut;
        private IRejonyDAO _RejonyDao;

        [SetUp]
        public void SetUp()
        {
            _RejonyDao = Substitute.For<IRejonyDAO>();
            _Sut = new RejonyService(_RejonyDao);
        }
        [Test]
        public void PobierzRejony()
        {
            List<KatRejony> RejonyZDb = new List<KatRejony>();

            _RejonyDao.PobieraczRejonow().Returns(RejonyZDb);
            List<KatRejony> Result = _Sut.PobierzRejony();
            Assert.AreSame(RejonyZDb, Result);

            _RejonyDao.Received(1).PobieraczRejonow();
        }
        [Test]
        public void PobierzRejonyDlaFirmy()
        {
            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = new KatLoginyFirmy()
                {
                    Firma = "Jagrol"
                }
            };
            List<KatRejony> RejonyZDb = new List<KatRejony>();
            _RejonyDao.PobieraczRejonowDlaFirmy("Jagrol").Returns(RejonyZDb);
            List<KatRejony> Result = _Sut.PobierzRejonyDlaFirmy(sesja);

            Assert.AreSame(RejonyZDb, Result);

            _RejonyDao.Received(1).PobieraczRejonowDlaFirmy("Jagrol");
        }
        [Test]
        public void WyszukajRejon()
        {
            List<KatRejony> RejonyZBazy = new List<KatRejony>()
            {
                new KatRejony() {Firma = "TopGen", Rejon = "33", Nazwa = "Kotyny" },
                new KatRejony() {Firma = "TopGen", Rejon = "23", Nazwa = "Psiarze"}
            };

            _RejonyDao.WyszukajRejon("TopGen", "szukam").Returns(RejonyZBazy);

            List<KatRejony> result = _Sut.WyszukajRejon("TopGen", "szukam");

            Assert.AreSame(result, RejonyZBazy);

            _RejonyDao.Received(1).WyszukajRejon("TopGen", "szukam");
        }
    }
}
