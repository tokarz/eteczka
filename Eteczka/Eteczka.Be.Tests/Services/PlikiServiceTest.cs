using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace Eteczka.BE.Services
{
    [TestFixture]
    public class PlikiServiceTest
    {

        private PlikiService _Sut;

        [SetUp]
        public void setUp()
        {
            _Sut = new PlikiService();
        }

        [Test]
        public void foo()
        {
        }

    }
}
