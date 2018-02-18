using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using Eteczka.BE.Services;

namespace Eteczka.Be.Tests.Services
{
    [TestFixture]
    public class KoszykServiceTest
    {
        private IKoszykService _Sut;
        private IKoszykDAO _KoszykDao;

        [SetUp]
        public void setUp()
        {
            _KoszykDao = Substitute.For<IKoszykDAO>();
            _Sut = new KoszykService(_KoszykDao);
        }
        [Test]
        public void PobierzIloscPlikowWKoszyku()
        {
            KatLoginyFirmy aktywnyUser = new KatLoginyFirmy()
            {
                Firma = "AFM",
                Identyfikator = "Ochódzka"
            };
            _KoszykDao.Policz(aktywnyUser).Returns(5);

            int result = _Sut.PobierzIloscPlikowWKoszyku(aktywnyUser);
            Assert.AreEqual(5, result);

            _KoszykDao.Received(1).Policz(aktywnyUser);
        }

        [Test]
        public void PobierzPlikiWKoszyku()
        {
            KatLoginyFirmy aktywnyUser = new KatLoginyFirmy()
            {
                Firma = "AFM",
                Identyfikator = "Ochódzka"
            };
            List<Pliki> PlikiZDb = new List<Pliki>();
            Pliki plikPierwszy = new Pliki();
            PlikiZDb.Add(plikPierwszy);
            Pliki plikDrugi = new Pliki();
            PlikiZDb.Add(plikDrugi);

            _KoszykDao.PobierzZawartoscKoszyka(aktywnyUser).Returns(PlikiZDb);
            List<Pliki> Result = _Sut.PobierzPlikiWKoszyku(aktywnyUser);

            Assert.AreEqual(PlikiZDb, Result);

            _KoszykDao.Received(1).PobierzZawartoscKoszyka(aktywnyUser);
        }

        [Test]
        public void DodajPlikiDoKoszyka()
        {
            KatLoginyFirmy aktywnyUser = new KatLoginyFirmy()
            {
                Firma = "AFM",
                Identyfikator = "Ochódzka"
            };

            List<string> PlikiId = new List<string>();
            string plikPierwszy = "aaa\\bbb.pdf";
            PlikiId.Add(plikPierwszy);
            string plikDrugi = "bbb\\aaa.pdf";
            PlikiId.Add(plikDrugi);

            _KoszykDao.DodajPlikiDoKoszyka(aktywnyUser, PlikiId).Returns(true);
             Assert.IsTrue(_Sut.DodajPlikiDoKoszyka(aktywnyUser, PlikiId));
            _KoszykDao.Received(1).DodajPlikiDoKoszyka(aktywnyUser, PlikiId);
        }
        [Test]
        public void UsunZKoszyka()
        {
            KatLoginyFirmy aktywnyUser = new KatLoginyFirmy()
            {
                Firma = "AFM",
                Identyfikator = "Ochódzka"
            };

            List<string> PlikiId = new List<string>();
            string plikPierwszy = "aaa\\bbb.pdf";
            PlikiId.Add(plikPierwszy);
            string plikDrugi = "bbb\\aaa.pdf";
            PlikiId.Add(plikDrugi);

            _KoszykDao.UsunZKoszyka(aktywnyUser, PlikiId).Returns(true);
            Assert.IsTrue(_Sut.UsunZKoszyka(aktywnyUser, PlikiId));
            _KoszykDao.Received(1).UsunZKoszyka(aktywnyUser, PlikiId);
        }
        [Test]
        public void WyczyscKoszyk()
        {
            KatLoginyFirmy aktywnyUser = new KatLoginyFirmy()
            {
                Firma = "AFM",
                Identyfikator = "Ochódzka"
            };
            _KoszykDao.WyczyscKoszyk(aktywnyUser).Returns(true);
            Assert.IsTrue(_Sut.WyczyscKoszyk(aktywnyUser));
            _KoszykDao.Received(1).WyczyscKoszyk(aktywnyUser);
        }


    }

    
}

