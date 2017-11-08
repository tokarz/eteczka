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
            KatLoginyDetale aktywnyUser = new KatLoginyDetale()
            {
                Identyfikator = "Ochódzka"
            };
            _KoszykDao.Policz("AFM", aktywnyUser).Returns(5);

            int result = _Sut.PobierzIloscPlikowWKoszyku("AFM", aktywnyUser);
            Assert.AreEqual(5, result);

            _KoszykDao.Received(1).Policz("AFM", aktywnyUser);
        }

        [Test]
        public void PobierzPlikiWKoszyku()
        {
            KatLoginyDetale aktywnyUser = new KatLoginyDetale()
            {
                Identyfikator = "Ochódzka"
            };
            List<Pliki> PlikiZDb = new List<Pliki>();
            Pliki plikPierwszy = new Pliki();
            PlikiZDb.Add(plikPierwszy);
            Pliki plikDrugi = new Pliki();
            PlikiZDb.Add(plikDrugi);

            _KoszykDao.PobierzZawartoscKoszyka("AFM", aktywnyUser).Returns(PlikiZDb);
            List<Pliki> Result = _Sut.PobierzPlikiWKoszyku("AFM", aktywnyUser);

            Assert.AreEqual(PlikiZDb, Result);

            _KoszykDao.Received(1).PobierzZawartoscKoszyka("AFM", aktywnyUser);
        }

        [Test]
        public void DodajPlikiDoKoszyka()
        {
            KatLoginyDetale aktywnyUser = new KatLoginyDetale()
            {
                Identyfikator = "Ochódzka"
            };

            List<string> PlikiId = new List<string>();
            string plikPierwszy = "aaa\\bbb.pdf";
            PlikiId.Add(plikPierwszy);
            string plikDrugi = "bbb\\aaa.pdf";
            PlikiId.Add(plikDrugi);

            _KoszykDao.DodajPlikiDoKoszyka("AFM", aktywnyUser, PlikiId).Returns(true);
             Assert.IsTrue(_Sut.DodajPlikiDoKoszyka("AFM", aktywnyUser, PlikiId));
            _KoszykDao.Received(1).DodajPlikiDoKoszyka("AFM", aktywnyUser, PlikiId);
        }
        [Test]
        public void UsunZKoszyka()
        {
            KatLoginyDetale aktywnyUser = new KatLoginyDetale()
            {
                Identyfikator = "Ochódzka"
            };

            List<string> PlikiId = new List<string>();
            string plikPierwszy = "aaa\\bbb.pdf";
            PlikiId.Add(plikPierwszy);
            string plikDrugi = "bbb\\aaa.pdf";
            PlikiId.Add(plikDrugi);

            _KoszykDao.UsunZKoszyka("AFM", aktywnyUser, PlikiId).Returns(true);
            Assert.IsTrue(_Sut.UsunZKoszyka("AFM", aktywnyUser, PlikiId));
            _KoszykDao.Received(1).UsunZKoszyka("AFM", aktywnyUser, PlikiId);
        }
        [Test]
        public void WyczyscKoszyk()
        {
            KatLoginyDetale aktywnyUser = new KatLoginyDetale()
            {
                Identyfikator = "Ochódzka"
            };
            _KoszykDao.WyczyscKoszyk("AFM", aktywnyUser).Returns(true);
            Assert.IsTrue(_Sut.WyczyscKoszyk("AFM", aktywnyUser));
            _KoszykDao.Received(1).WyczyscKoszyk("AFM", aktywnyUser);
        }


    }

    
}

