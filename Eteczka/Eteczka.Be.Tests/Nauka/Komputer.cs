using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Be.Tests.Nauka
{
    [TestFixture]
    public class Komputer_Test
    {
        private Kalkulator _Kalk;
        private Komputer _Sut;

        [SetUp]

        public void Setup()
        {

            _Kalk = Substitute.For<Kalkulator>();
            _Sut = new Komputer(_Kalk);
        }
        [Test]

        public void ObliczDelta_test()
        {

            _Kalk.Pomnoz(1, 1).Returns(1);
            _Kalk.Pomnoz(1, 2).Returns(2);
            _Kalk.Pomnoz(4, 2).Returns(8);
            _Kalk.Dodaj(1, 8).Returns(9);

            int result = _Sut.ObliczDelta(1, 2);
            Assert.AreEqual(9, result);
        }

    }

    public class Komputer
    {
        private Kalkulator _Kalkulator;

        public Komputer(Kalkulator kalkulator)
        {
            _Kalkulator = kalkulator;
        }

        // a*a + 4ab
        public virtual int ObliczDelta(int a, int b)
        {
            int akwadrat = _Kalkulator.Pomnoz(a, a);
            int ab = _Kalkulator.Pomnoz(a, b);
            int czteryAb = _Kalkulator.Pomnoz(4, ab);
            int result = _Kalkulator.Dodaj(akwadrat, czteryAb);

            return result;
        }

    }
}
