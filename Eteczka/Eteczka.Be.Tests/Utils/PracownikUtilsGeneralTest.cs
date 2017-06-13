using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Utils
{

    [TestFixture]
    public class PracownikUtilsGeneralTest
    {
        // Zobacz! Kiedys napisal bys:
        // private PracownikUtils _Sut;
        // wtedy tylko ten typ moglbys zainicjalizowac poprzez new : _Sut = new PracownikUtils()
        //Gdy uzyjesz interfejsu (albo klasy bazowej przy dziedziczeniu ale to potem) mozesz uzyc skladni:

        // TypOgolny x = new Typ1()
        // TypOgolny y = new Typ2()

        //I teraz mozesz uzyc tego do konfiguracji swojego programu

        //np Program pyta: chcesz Dokladnie sprawdzic czy nie pliki
        // string odpowiedz = wczytajOdpowiedzZKlawiatury();
        // if (odpowiedz.equals("T") {
        // x = newTyp1();
        // } else { x = newTyp2();} 



        private WyszukiwaczPlikow _Sut;
        private WyszukiwaczPlikow _DokladnySut;
        private IMapowalnyDoPracownikDto _Mapper;

        [SetUp]
        public void Init()
        {
            _Mapper = new PracownikDtoMapper();
            _Sut = new PracownikUtils(_Mapper);
            _DokladnySut = new DokladnyPracownikUtils();
        }


        [Test]
        public void ZnajdzPracownikowZPlikiem_InnyObiekt()
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            List<string> listaZPlikiem = new List<string>();

            listaZPlikiem.Add("jakisInnyPlik");
            listaZPlikiem.Add("d:/jakisPlik");

            List<string> listaBezPliku = new List<string>();
            listaBezPliku.Add("jakisInnyPlik");
            listaBezPliku.Add("c:/jakisPlik");


            Pracownik first = new Pracownik()
            {
                Id = "0",
                Pliki = listaZPlikiem
            };
            Pracownik second = new Pracownik()
            {
                Id = "1",
                Pliki = listaBezPliku
            };
            Pracownik third = new Pracownik()
            {
                Id = "2",
                Pliki = listaZPlikiem
            };
            Pracownik fourth = new Pracownik()
            {
                Id = "3",
                Pliki = listaZPlikiem
            };

            pracownicy.Add(first);
            pracownicy.Add(second);
            pracownicy.Add(third);
            pracownicy.Add(fourth);

            List<PracownikDTO> result = _Sut.ZnajdzPracownikowZPlikiem("jakisPlik", pracownicy);
            List<PracownikDTO> dokladnyResult = _DokladnySut.ZnajdzPracownikowZPlikiem("d:/jakisPlik", pracownicy);

            Assert.NotNull(result);
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual("0", result[0].Id);
            Assert.AreEqual("1", result[1].Id);
            Assert.AreEqual("2", result[2].Id);
            Assert.AreEqual("3", result[3].Id);

            Assert.NotNull(dokladnyResult);
            Assert.AreEqual(3, dokladnyResult.Count);

            Assert.AreEqual("0", dokladnyResult[0].Id);
            Assert.AreEqual("2", dokladnyResult[1].Id);
            Assert.AreEqual("3", dokladnyResult[2].Id);

        }


    }



}