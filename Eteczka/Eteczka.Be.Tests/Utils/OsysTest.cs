using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace Eteczka.BE.Utils
{
    [TestFixture]
    public class OsysTest
    {

        private Osys _Sut;

        [SetUp]
        public void Init()
        {
            _Sut = new Osys();
        }


        [Test]
        public void SprawdzIban()
        {
            Assert.IsFalse(_Sut.SprawdzIban("Konto"));
            Assert.IsFalse(_Sut.SprawdzIban("KontoDluzszeNiz12ZnakowAleNieprawidloweWiecPowinnoZwrocicFalse"));
            Assert.IsFalse(_Sut.SprawdzIban("15 1140 2004 0000 3602 3569 3944"));

            Assert.IsTrue(_Sut.SprawdzIban("39 1140 2004 0000 3602 3569 3944"));
            Assert.IsTrue(_Sut.SprawdzIban("70 1090 2154 0000 0005 6000 2210"));
        }

        [Test]
        public void SprawdzIbanPoElementach()
        {
            Assert.IsFalse(_Sut.SprawdzIbanPoElementach("Konto"));
            Assert.IsFalse(_Sut.SprawdzIbanPoElementach("KontoDluzszeNiz12ZnakowAleNieprawidloweWiecPowinnoZwrocicFalse"));
            Assert.IsFalse(_Sut.SprawdzIbanPoElementach("15 1140 2004 0000 3602 3569 3944"));

            Assert.IsTrue(_Sut.SprawdzIbanPoElementach("39 1140 2004 0000 3602 3569 3944"));
            Assert.IsTrue(_Sut.SprawdzIbanPoElementach("70 1090 2154 0000 0005 6000 2210"));
        }

        [Test]
        public void SprawdzPesel()
        {
            Assert.IsFalse(_Sut.SprawdzPesel("K", "85101717855"));
            Assert.IsFalse(_Sut.SprawdzPesel("M", "85101714854"));
            Assert.IsFalse(_Sut.SprawdzPesel("M", "85101714855"));

            Assert.IsTrue(_Sut.SprawdzPesel("M", "85101717855"));

            Assert.IsTrue(_Sut.SprawdzPesel("K", "12272707680"));
            Assert.IsTrue(_Sut.SprawdzPesel("M", "09280108163"));
            Assert.IsTrue(_Sut.SprawdzPesel("K", "04241609930"));
        }

        [Test]
        public void PlecZPesela()
        {
            Assert.AreEqual("M", _Sut.PeselOddajPlec("85101717855"));
            Assert.AreEqual("M", _Sut.PeselOddajPlec("85101714854"));
            Assert.AreEqual("M", _Sut.PeselOddajPlec("85101714855"));

            Assert.AreEqual("M", _Sut.PeselOddajPlec("85101717855"));

            Assert.AreEqual("K", _Sut.PeselOddajPlec("12272707680"));
            //Assert.AreEqual("M", _Sut.PeselOddajPlec("09280108163"));
            //Assert.AreEqual("K", _Sut.PeselOddajPlec("04241609930"));
            Assert.AreEqual("K", _Sut.PeselOddajPlec("85112510465"));
        }

        [Test]
        public void DataZPesela()
        {
            Assert.AreEqual("19851017", _Sut.PeselOddajDate("85101717855"));
            Assert.AreEqual("19851125", _Sut.PeselOddajDate("85112510465"));
            //Assert.AreEqual("20120727", _Sut.PeselOddajDate("12272707680"));
        }

        [Test]
        public void kalendarzKoniecMiesiaca()
        {
            Assert.AreEqual("1985-10-31", _Sut.kalendarzKoniecMiesiaca("1985", "10"));
        }

        [Test]
        public void testHasel()
        {
            Dictionary<string, string> hasla = new Dictionary<string, string>();
            bool result = false;
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    string haslo = _Sut.hasloGeneruj();
                    hasla.Add(haslo, "exists");
                    Assert.AreEqual(12, haslo.Length);
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            Assert.IsTrue(result);

        }
    }
}
