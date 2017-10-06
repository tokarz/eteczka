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
                    AktywnaFirma = "JakasAktywnaFirma ze Zgierza"
                };

            //Przed wywolaniem robimy "probe". Mowimy aktorom (Mockom) co maja zrobic, jesli zostana w taki (dokladnie taki!) sposob wywolani:

            _PracownikDao.PobierzPracownikow("JakasAktywnaFirma ze Zgierza").Returns(pracownicyOtrzymaniZBe);

            //Przedstawienie!

            List<Pracownik> retultatTestu = _Sut.PobierzWszystkich(jakasSesja);

            //A teraz do akcji wchodza krytycy:) Oceniaja i sprawdzaja sztuke i aktorow!:)

            //Czy wynik sie zgadza?
            Assert.AreSame(pracownicyOtrzymaniZBe, retultatTestu);

            //Czy aktor odegral swoja role?
            _PracownikDao.Received(1).PobierzPracownikow("JakasAktywnaFirma ze Zgierza");
        }

    }
}
