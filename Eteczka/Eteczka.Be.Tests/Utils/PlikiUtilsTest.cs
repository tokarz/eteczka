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

        [Test]
        public void WezPlikiZRozszerzeniem()
        {
            List<string> sciezkiTxt = new List<string>();
            sciezkiTxt.Add("d:/kat1/1.txt");
            sciezkiTxt.Add("d:\\kat1\\2.txt");
            sciezkiTxt.Add("d:/kat1");
            sciezkiTxt.Add("d:/kat1/4.txt");
            sciezkiTxt.Add("d:/kat1/4.tx/t");
            sciezkiTxt.Add("d:/kat1/4.tx/a.zip");

            List<string> fileNames = new List<string>();
            fileNames.Add("1.txt");
            fileNames.Add("2.txt");
            fileNames.Add("4.txt");

            Assert.AreEqual(fileNames, _Sut.WezPlikiZRozszerzeniem(sciezkiTxt, "txt"));

            List<string> zips = new List<string>();
            zips.Add("a.zip");

            Assert.AreEqual(fileNames, _Sut.WezPlikiZRozszerzeniem(sciezkiTxt, "txt"));
            Assert.AreEqual(zips, _Sut.WezPlikiZRozszerzeniem(sciezkiTxt, "zip"));
        }


        [Test]
        public void WezSpolneElementy()
        {
            List<string> plikiA = new List<string>();
            List<string> plikiB = new List<string>();

            List<string> result = _Sut.WezSpolneElementy(plikiA, plikiB);
            Assert.IsTrue(result.Count == 0);

            //result = _Sut.WezSpolneElementy(null, null);
            //Assert.IsTrue(result.Count == 0);


            plikiA.Add("c:/plik1.bat");
            plikiA.Add("d:/plik2.txt");
            plikiA.Add("d:/plik1.exe");

            plikiB.Add("c:/plik1.bat");
            plikiB.Add("d:/plik1.exe");
            plikiB.Add("d:/plik2.com");


            result = _Sut.WezSpolneElementy(plikiA, plikiB);
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual("plik1.bat", result[0]);
            Assert.AreEqual("plik1.exe", result[1]);

        }

        [Test]
        public void PoliczRozszerzenia()
        {
            List<string> pliki = new List<string>();
            pliki.Add("d:/plik1.txt");
            pliki.Add("d:/plik2.txt");
            pliki.Add("d:/plik1.pdf");
            pliki.Add("d:/plik1.pdf");
            pliki.Add("d:/plik1.zip");
            pliki.Add("d:/plik1.rar");

            Dictionary<string, int> result = _Sut.PoliczRozszerzenia(pliki);
           
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(2, result["txt"]);
            Assert.AreEqual(2, result["pdf"]);
            Assert.AreEqual(1, result["zip"]);
            Assert.AreEqual(1, result["rar"]);


        }


    }
}
