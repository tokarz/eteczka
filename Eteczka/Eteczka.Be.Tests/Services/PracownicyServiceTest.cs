using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.DAO;

namespace Eteczka.BE.Services
{
    [TestFixture]
    public class PracownicyServiceTest
    {

        //private PracownicyService _Sut;
        //private UserDAO _UserDao;
        //private PracownikDAO _PracownikDao;

        [SetUp]
        public void setUp()
        {
            //_UserDao = Substitute.For<UserDAO>(null);
            //_PracownikDao = Substitute.For<PracownikDAO>();
            //_Sut = new PracownicyService(_UserDao, _PracownikDao);
        }

        [Test]
        public void foo()
        {
            //_Sut.ImportujJson("someSessionId");
        }

    }
}
