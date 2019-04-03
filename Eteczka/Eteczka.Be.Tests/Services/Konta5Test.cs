//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using NSubstitute;
//using Eteczka.BE.Services;
//using Eteczka.DB.DAO;
//using Eteczka.Model.Entities;

//namespace Eteczka.Be.Tests.Services
//{
//    [TestFixture]
//    public class Konta5Test
//    {
//        private IKonto5Service _sut;
//        private Konto5DAO Dao;

//        [SetUp]
//        public void Setup()
//        {
//            Dao = Substitute.For<Konto5DAO>();
//            _sut = new Konto5Service(Dao);
//        }
//        [Test]
//        public void WyszukajKonto5()
//        {
//            List<KatKonto5> kontaZBazy = new List<KatKonto5>()
//            {
//                new KatKonto5 {Nazwa = "Ziemniaki pastwne", Konto5 = "HT2938"},
//                new KatKonto5 { Nazwa = "Farmy ziemniaczane", Konto5 = "FR4838"}
//            };

//            Dao.WyszukajKonto5("TopGen", "ziemn").Returns(kontaZBazy);

//            List<KatKonto5> WyszukaneKonta = _sut.WyszukajKonto5("TopGen", "ziemn");

//            Assert.AreSame(WyszukaneKonta, kontaZBazy);
//            Dao.Received(1).WyszukajKonto5("TopGen", "ziemn");
//        }
//    }
//}
