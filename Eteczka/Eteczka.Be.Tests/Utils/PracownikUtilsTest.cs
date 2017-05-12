using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.BE.DTO;




namespace Eteczka.BE.Utils
{

    [TestFixture]
    public class PracownikUtilsTest
    {
        private PracownikUtils _Sut;

        [SetUp]
        public void Init()
        {
            _Sut = new PracownikUtils();
        }



        [Test]
        public void ZnajdzPracownikowZPlikiem()
        {
            List<string> listaZPlikiem = new List<string>();
            listaZPlikiem.Add("jakisInnyPlik");
            listaZPlikiem.Add("d:/jakisPlik");

            List<string> listaBezPliku = new List<string>();
            listaZPlikiem.Add("jakisInnyPlik");
            listaZPlikiem.Add("c:/jakisPlik");

            List<PracownikDTO> pracownicy = new List<PracownikDTO>();
            PracownikDTO prac1 = new PracownikDTO();
            prac1.Id = "0";
            prac1.Pliki = listaZPlikiem;

            PracownikDTO prac2 = new PracownikDTO();
            prac2.Id = "1";
            prac2.Pliki = listaZPlikiem;

            PracownikDTO prac3 = new PracownikDTO();
            prac3.Id = "2";
            prac3.Pliki = listaBezPliku;

            PracownikDTO prac4 = new PracownikDTO();
            prac4.Id = "3";
            prac4.Pliki = listaZPlikiem;

            pracownicy.Add(prac1);
            pracownicy.Add(prac2);
            pracownicy.Add(prac3);
            pracownicy.Add(prac4);

            List<PracownikDTO> result =  _Sut.ZnajdzPracownikowZPlikiem("d:/jakisPlik", pracownicy);

            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);

            Assert.AreEqual("0", result[0].Id);
            Assert.AreEqual("1", result[1].Id);
            Assert.AreEqual("3", result[2].Id);

        }


    }



}