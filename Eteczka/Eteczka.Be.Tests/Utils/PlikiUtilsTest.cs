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
    public class PlikiUtilsTest
    {

        private PlikiUtils _Sut;

        [SetUp]
        public void Init()
        {
            _Sut = new PlikiUtils();
        }


        [Test]
        public void GetPath()
        {
            Assert.AreEqual("plik.txt", _Sut.WezNazwePlikuZeSciezki("c:/dupa/plik.txt"));
            Assert.AreEqual("", _Sut.WezNazwePlikuZeSciezki("c:\\dupa"));
            Assert.AreEqual("", _Sut.WezNazwePlikuZeSciezki(""));
            Assert.AreEqual("plik.txt", _Sut.WezNazwePlikuZeSciezki("c:\\dupa\\plik.txt"));
        }


        

    }
}
