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

        [Test]
        public void WezNazwePlikowZeSciezek()
        {
            List<string> sciezki = new List<string>();
            sciezki.Add("d:/kat1/1.txt");
            sciezki.Add("d:\\kat1\\2.txt");
            sciezki.Add("d:/kat1");
            sciezki.Add("d:/kat1/4.txt");
            sciezki.Add("d:/kat1/4.tx/t");
                
            List<string> fileNames = new List<string>();
            fileNames.Add("1.txt");
            fileNames.Add("2.txt");
            fileNames.Add("4.txt");

            Assert.AreEqual(fileNames, _Sut.WezNazwePlikowZeSciezek(sciezki));
        }


    }
}
