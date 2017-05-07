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
    public class JrwaParserTest
    {
        private JrwaParser _Sut;

        [SetUp]
        public void Init()
        {
            _Sut = new JrwaParser();
        }

        [Test]
        public void WczytajStrukture()
        {
            _Sut.WczytajStrukture("d:/eteczka.main/strukturaFirmy/jrwa.xml");
        }
    }
}
