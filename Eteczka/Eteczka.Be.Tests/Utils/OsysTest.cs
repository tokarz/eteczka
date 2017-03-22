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

            Assert.IsTrue(_Sut.SprawdzIban("PL67 1234 5678 0000 0000 1234 5678"));
        }
    }
}
