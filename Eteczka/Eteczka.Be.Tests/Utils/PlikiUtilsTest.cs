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

        [Test]
        public void PoliczPlikiWKatalogach()
        {
            List<string> pliki = new List<string>();
            pliki.Add("d:/A/plik1.txt");
            pliki.Add("d:/A/plik2.txt");
            pliki.Add("d:/A/plik1.pdf");
            pliki.Add("d:/B/plik1.pdf");
            pliki.Add("d:/B/plik1.zip");
            pliki.Add("d:/B/plik1.rar");
            pliki.Add("d:\\plik1.rar");

            Dictionary<string, List<string>> result = _Sut.PoliczPlikiWKatalogach(pliki);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, result["d:/A/"].Count);
            Assert.AreEqual(3, result["d:/B/"].Count);
            Assert.AreEqual(1, result["d:\\"].Count);

        }

        [Test]
        public void ZnajdzPlikiPoNazwie()
        {

            List<string> pliki = new List<string>();
            pliki.Add("d:/A/plik1.txt");
            pliki.Add("d:/A/x.txt");
            pliki.Add("d:/A/y.pdf");
            pliki.Add("d:/B/z.pdf");
            pliki.Add("d:/B/pl1ik.zip");
            pliki.Add("d:/B/plik1.rar");
            pliki.Add("d:\\plikOFajnejNazwie.rar");

            List<string> znalezionePliki = _Sut.ZnajdzPlikiPoNazwie("pli", pliki);

            Assert.IsNotNull(znalezionePliki);
            Assert.IsNotEmpty(znalezionePliki);

            Assert.AreEqual(3, znalezionePliki.Count);
            Assert.AreEqual("d:/A/plik1.txt", znalezionePliki[0]);
            Assert.AreEqual("d:/B/plik1.rar", znalezionePliki[1]);
            Assert.AreEqual("d:\\plikOFajnejNazwie.rar", znalezionePliki[2]);

        }

        [Test]
        public void WczytajPlik()
        {
            string zawartoscReadmeTxt = "WITAJ SWIECIE!";

            string result = _Sut.WczytajPlik("../../test-data/README.txt");

            Assert.AreEqual(zawartoscReadmeTxt, result);
        }


        [Test]
        public void WczytajOsoby()
        {
            List<string> osoby = new string[] { "Maciej Tokarz", "Michal Skalacki", "Aleksandra Tokarz", "Zbigniew Tokarz" }.ToList<string>();

            List<string> result = _Sut.WczytajPlikiZFolderu("../../test-data/osoby", "txt");

            Assert.AreEqual(4, result.Count);

        }

        [Test]
        public void ExcellWczytajWiersz()
        {
            string sciezkaDoPliku = "../../test-data/excell/Rodzaje_dokumentow_Eteczka.xlsx";

            List<string> result = _Sut.ExcellWczytajWiersz(sciezkaDoPliku, 1, 1);

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("nazwa dokumentu", result[0]);
            Assert.AreEqual("symbol dokumentu", result[1]);
            Assert.AreEqual("część akt", result[2]);

            result = _Sut.ExcellWczytajWiersz(sciezkaDoPliku, 1, 2);

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("kwestionariusz osobowy kandydata", result[0]);
            Assert.AreEqual("KwOsKand", result[1]);
            Assert.AreEqual("A", result[2]);

        }

        [Test]
        public void ExcellWczytajKatDok()
        {
            //string sciezkaDoPliku = "../../test-data/excell/Rodzaje_dokumentow_Eteczka_Full.xlsx";
            //ExcelKatDok result = _Sut.ExcellWczytajKatDok(sciezkaDoPliku, 1);

            //Assert.IsNotNull(result);
            //Assert.AreEqual(3, result.Naglowek.Count);
            //Assert.AreEqual("nazwa dokumentu", result.Naglowek[0]);
            //Assert.AreEqual("symbol dokumentu", result.Naglowek[1]);
            //Assert.AreEqual("część akt", result.Naglowek[2]);

            //Assert.AreEqual(71, result.CalyPlik.Count);

            //Assert.AreEqual("nazwa dokumentu", result.CalyPlik[0].NazwaDokumentu);
            //Assert.AreEqual("symbol dokumentu", result.CalyPlik[0].SymbolDokumentu);
            //Assert.AreEqual("część akt", result.CalyPlik[0].CzescAkt);

            //Assert.AreEqual("kwestionariusz osobowy kandydata", result.CalyPlik[1].NazwaDokumentu);
            //Assert.AreEqual("KwOsKand", result.CalyPlik[1].SymbolDokumentu);
            //Assert.AreEqual("A", result.CalyPlik[1].CzescAkt);

            //Assert.AreEqual("informacja o karalności", result.CalyPlik[70].NazwaDokumentu);
            //Assert.AreEqual("InfKrk", result.CalyPlik[70].SymbolDokumentu);
            //Assert.AreEqual("B", result.CalyPlik[70].CzescAkt);
        }


    }
}
