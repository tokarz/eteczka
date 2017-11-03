using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToPracownikMapperTest
    {
        private IJsonToPracownikMapper _Sut;
        string FULL_VALID_JSON = @"{
			""imie"" : ""IRENA"",
			""nazwisko"" : ""BZDYRA"",
			""pesel"" : ""50122512688"",
			""numeread"" : ""BZDIRE50122512688"",
			""kraj"" : ""PL"",
			""nazwiskorodowe"" : ""POMSCIWODA"",
			""imiematki"" : ""HELENA"",
			""imieojca"" : ""KAZIMIERZ"",
			""peselinny"" : ""213321"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""datamodify"" : ""2017.08.26 12:35:14"",
			""dataakcept"" : ""2017.08.26 14:35:14"",
			""dataurodzenia"" : ""1955-12-25"",
			""imie2"" : ""ADELAJDA"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""     
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToPracownikMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            Pracownik pracownik = _Sut.Map(parsedJson);

            Assert.AreEqual("IRENA", pracownik.Imie);
            Assert.AreEqual("BZDYRA", pracownik.Nazwisko);
            Assert.AreEqual("50122512688", pracownik.PESEL);
            Assert.AreEqual("BZDIRE50122512688", pracownik.Numeread);
            Assert.AreEqual("PL", pracownik.Kraj);
            Assert.AreEqual("POMSCIWODA", pracownik.NazwiskoRodowe);
            Assert.AreEqual("HELENA", pracownik.ImieMatki);
            Assert.AreEqual("KAZIMIERZ", pracownik.ImieOjca);
            Assert.AreEqual("213321", pracownik.PeselInny);
            Assert.AreEqual("Administrator", pracownik.IdOper);
            Assert.AreEqual("Administrator2", pracownik.IdAkcept);
            Assert.AreEqual(DateTime.Parse("2017.08.26 12:35:14"), pracownik.DataModify);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:35:14"), pracownik.DataAkcept);
            Assert.AreEqual("1955-12-25", pracownik.DataUrodzenia);
            Assert.AreEqual("ADELAJDA", pracownik.Imie2);
            Assert.AreEqual("VFP", pracownik.SystemBazowy);
            Assert.AreEqual(false, pracownik.Usuniety);

        }
    }
}
