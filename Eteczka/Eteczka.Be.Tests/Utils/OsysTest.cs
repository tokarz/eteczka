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
            Assert.IsTrue(_Sut.SprawdzPesel("K", "09280108163"));
            Assert.IsTrue(_Sut.SprawdzPesel("M", "04241609930"));


        }


    }
}
