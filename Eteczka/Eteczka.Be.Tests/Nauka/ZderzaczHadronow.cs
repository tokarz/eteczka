using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.Be.Tests.Nauka
{
    public class ZderzaczHadronow_Test
    {
        private ZderzaczHadronow _Sut;
        private Komputer _Komp;

        [SetUp]

        public void setup()
        {

            _Komp = Substitute.For<Komputer>(new Kalkulator());
            _Sut = new ZderzaczHadronow(_Komp);
           
        }

        [Test]

        public void ZderzHadrony_BOOM()
        {
            _Komp.ObliczDelta(1, 2).Returns(123);
            _Komp.ObliczDelta(2, 1).Returns(321);
            _Komp.ObliczDelta(123, 321).Returns(4);
            string result = _Sut.ZderzHadrony(1, 2);
            Assert.AreEqual("BOOM!", result);
        }

        [Test]
        public void ZderzHadrony_PUDLO()
        {
            _Komp.ObliczDelta(1, 2).Returns(123);
            _Komp.ObliczDelta(2, 1).Returns(321);
            _Komp.ObliczDelta(123, 321).Returns(3);
            string result = _Sut.ZderzHadrony(1, 2);
            Assert.AreEqual("PUDLO!", result);
        }
    }

    public class ZderzaczHadronow
    {
        private Komputer _SuperKomputer;

        public ZderzaczHadronow(Komputer komputer)
        {
            _SuperKomputer = komputer;
        }

        public string ZderzHadrony(int hadron, int hadronica)
        {
            int result_1 = _SuperKomputer.ObliczDelta(hadron, hadronica);
            int result_2 = _SuperKomputer.ObliczDelta(hadronica, hadron);

            int superResult = _SuperKomputer.ObliczDelta(result_1, result_2);

            if(superResult %2 == 0)
            {
                return "BOOM!";
            }
            else
            {
                return "PUDLO!";
            }

        }
    }
}
