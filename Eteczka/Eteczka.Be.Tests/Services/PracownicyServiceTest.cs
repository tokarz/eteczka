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

namespace Eteczka.BE.Services
{
    [TestFixture]
    public class PracownicyServiceTest
    {

        private IPracownicyService _Sut;
        private IPracownikDAO _PracownikDao;

        [SetUp]
        public void setUp()
        {
            //Tworzymy Mocka (zaslepke obiektu)
            _PracownikDao = Substitute.For<IPracownikDAO>();

            //Inicjujemy nasza testowana klase razem z Mockiem (Mackiem?)
            _Sut = new PracownicyService(_PracownikDao);
        }

        [Test]
        public void PobierzWszystkich()
        {
            List<Pracownik> pracownicyOtrzymaniZBe = new List<Pracownik>() { new Pracownik() {
                Imie = "Irena",
                Nazwisko = "Ochodzka"
            }};
            SessionDetails jakasSesja = new SessionDetails()
            {
                AktywnaFirma = "JakasAktywnaFirma ze Zgierza",
                AktywnyUser = new KatLoginyDetale()
                {
                    Confidential = 10
                }
            };

            //Przed wywolaniem robimy "probe". Mowimy aktorom (Mockom) co maja zrobic, jesli zostana w taki (dokladnie taki!) sposob wywolani:

            _PracownikDao.PobierzPracownikow("JakasAktywnaFirma ze Zgierza", 10).Returns(pracownicyOtrzymaniZBe);

            //Przedstawienie!

            List<Pracownik> retultatTestu = _Sut.PobierzWszystkich(jakasSesja);

            //A teraz do akcji wchodza krytycy:) Oceniaja i sprawdzaja sztuke i aktorow!:)

            //Czy wynik sie zgadza?
            Assert.AreSame(pracownicyOtrzymaniZBe, retultatTestu);

            //Czy aktor odegral swoja role?
            _PracownikDao.Received(1).PobierzPracownikow("JakasAktywnaFirma ze Zgierza", 10);
        }

        [Test]

        public void PobierzWszystkichZatrudnionych()
        {
            List<Pracownik> ZatrudnieniZBe = new List<Pracownik>();

            Pracownik Ochodzka = new Pracownik()
            {
                Imie = "Irena",
                Nazwisko = "Ochódzka"
            };

            ZatrudnieniZBe.Add(Ochodzka);

            SessionDetails JakasSesja = new SessionDetails()
            {
                AktywnaFirma = "TFW",
                 AktywnyUser = new KatLoginyDetale()
                 {
                     Confidential = 10
                 }
            };

            _PracownikDao.PobierzZatrudnionychPracownikow("TFW", 10).Returns(ZatrudnieniZBe);

            List<Pracownik> RezultatTestu = _Sut.PobierzWszystkichZatrudnionych(JakasSesja);
            Assert.AreSame(ZatrudnieniZBe, RezultatTestu);
            _PracownikDao.Received(1).PobierzZatrudnionychPracownikow("TFW",10);
        }

        [Test]

        public void PobierzPoId()
        {

            Pracownik testowy = new Pracownik()
            {
                Numeread = "123456"
            };


            _PracownikDao.PobierzPracownikaPoId("123456").Returns(testowy);
            Pracownik pobrany = _Sut.PobierzPoId("123456");
            Assert.AreSame(testowy, pobrany);


            _PracownikDao.Received(1).PobierzPracownikaPoId("123456");

        }

        [Test]

        public void ZnajdzPracownikow()
        {
            List<Pracownik> Testowi = new List<Pracownik>();
            Pracownik testowy1 = new Pracownik()
            {
                Nazwisko = "Kargul"
            };

            Testowi.Add(testowy1);

            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = "TFG",
                 AktywnyUser = new KatLoginyDetale()
                 {
                     Confidential = 10
                 }
            };

            _PracownikDao.WyszukiwaczPracownikow("Kargul", "TFG",10).Returns(Testowi);

            List<Pracownik> Pobrani = _Sut.ZnajdzPracownikow("Kargul", sesja);
            Assert.AreSame(Testowi, Pobrani);


            _PracownikDao.Received(1).WyszukiwaczPracownikow("Kargul", "TFG",10);

        }
        [Test]
        public void PobierzPozostalych()
        {
            List<Pracownik> PracownicyZDb = new List<Pracownik>();
            Pracownik Ochodzka = new Pracownik()
            {
                Imie = "Irena",
                Nazwisko = "Ochodzka"
            };
            PracownicyZDb.Add(Ochodzka);

            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = "TFG",
                 AktywnyUser = new KatLoginyDetale()
                 {
                     Confidential = 10
                 }
            };
            _PracownikDao.PobierzPozostalychPracownikow("TFG",10).Returns(PracownicyZDb);

            List<Pracownik> PracownicyResult = _Sut.PobierzPozostalych(sesja);

            Assert.AreSame(PracownicyZDb, PracownicyResult);

            _PracownikDao.Received(1).PobierzPozostalychPracownikow("TFG",10);

        }
        [Test]
        public void ZnajdzPracownikowPoTekscie()
        {
            List<Pracownik> PracownicyZDb = new List<Pracownik>();
            Pracownik pracownikZDb = new Pracownik()
            {
                Nazwisko = "Kryniulak"
            };
            PracownicyZDb.Add(pracownikZDb);

            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = "AFM",
                AktywnyUser = new KatLoginyDetale()
                {
                    Confidential = 10
                }
            };
            _PracownikDao.WyszukiwaczPracownikowPoTekscie("Kryn", "AFM",10).Returns(PracownicyZDb);
            List<Pracownik> result = _Sut.ZnajdzPracownikowPoTekscie("Kryn", sesja);

            Assert.AreSame(PracownicyZDb, result);
            _PracownikDao.Received(1).WyszukiwaczPracownikowPoTekscie("Kryn", "AFM",10);
        }
        [Test]
        public void ZnajdzZatrPracownikowPoTekscie()
        {
            List<Pracownik> PracownicyZDb = new List<Pracownik>();

            Pracownik pracownikZDb = new Pracownik()
            {
                Nazwisko = "Kryniulak"
            };
            PracownicyZDb.Add(pracownikZDb);
            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = "TFG",
                AktywnyUser = new KatLoginyDetale()
                {
                    Confidential = 10
                }
            };

            _PracownikDao.WyszukiwaczZatrPracownikowPoTekscie("Szukam", "TFG",10).Returns(PracownicyZDb);
            List<Pracownik> result = _Sut.ZnajdzZatrPracownikowPoTekscie("Szukam", sesja);
            Assert.AreSame(PracownicyZDb, result);


            _PracownikDao.Received(1).WyszukiwaczZatrPracownikowPoTekscie("Szukam", "TFG",10);
        }
        [Test]
        public void ZnajdzPozostPracownikowPoTekscie()
        {
            List<Pracownik> PracownicyZDb = new List<Pracownik>();
            Pracownik pracownikZDb = new Pracownik()
            {
                Nazwisko = "JakiesNazwisko"
            };
            SessionDetails sesja = new SessionDetails()
            {
                AktywnaFirma = "AFM",
                AktywnyUser = new KatLoginyDetale()
                {
                    Confidential = 10
                }
            };
            _PracownikDao.WyszukiwaczPozostZatrPracownikowPoTekscie("Szukam", "AFM",10).Returns(PracownicyZDb);
            List<Pracownik> result = _Sut.ZnajdzPozostPracownikowPoTekscie("Szukam", sesja);
            Assert.AreSame(PracownicyZDb, result);

            _PracownikDao.Received(1).WyszukiwaczPozostZatrPracownikowPoTekscie("Szukam", "AFM",10);
        }
        //[Test]
        //public void DodajPracownika()
        //{
        //    InsertResult result = new InsertResult();




        //    Pracownik pracownikDoDodania = new Pracownik()
        //    {
        //        Nazwisko = "Testowy2",
        //        Imie = "Adam",
        //        PESEL = "88292929929"

        //    };
        //    SessionDetails sesja = new SessionDetails()
        //    {
        //        AktywnyUser = new KatLoginyDetale()
        //        {
        //            Identyfikator = "test"
        //        }
        //    };

        //    _PracownikDao.PobierzPracownikaPoId("TesAda88292929929").Returns(pracownikDoDodania);
        //    _PracownikDao.DodajPracownika(pracownikDoDodania, "test", "test").Returns(true);
        //    result = _Sut.DodajPracownika(pracownikDoDodania, sesja);

        //    Assert.AreEqual(false, result.Result);

        //}
    }
}
