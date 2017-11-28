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
    public class FirmyServiceTest
    {
        private IFirmyService _Sut;
        private IFirmyDAO _FirmyDao;

        [SetUp]
        public void SetUp()
        {
            _FirmyDao = Substitute.For<IFirmyDAO>();
            _Sut = new FirmyService(_FirmyDao);
        }
        [Test]
        public void PobierzWszystkie()
        {

            List<KatFirmy> FirmyZDb = new List<KatFirmy>();
            _FirmyDao.PobierzFirmyZBazy().Returns(FirmyZDb);
            List<KatFirmy> Result = _Sut.PobierzWszystkie();

            Assert.AreSame(FirmyZDb, Result);

            _FirmyDao.Received(1).PobierzFirmyZBazy();
        }


    }

}
