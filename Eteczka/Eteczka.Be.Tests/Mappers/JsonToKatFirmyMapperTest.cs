using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToKatFirmyMapperTest
    {
        private IJsonToKatFirmyMapper _Sut;
        private string FULL_VALID_JSON =
                  @"{
			        ""firma"" : ""TFG"",
			        ""nazwa"" : ""TOP FARMS GŁUBCZYCE SP. Z O.O."",
			        ""nazwaskrocona"" : ""TOP FARMS Głubczyce sp zoo"",
			        ""ulica"" : ""BOLESŁAWA CHROBREGO"",
			        ""numerdomu"" : ""23"",
			        ""numerlokalu"" : ""abc"",
			        ""miasto"" : ""MGŁUBCZYCE"",
			        ""kodpocztowy"" : ""48-100"",
			        ""poczta"" : ""GŁUBCZYCE"",
			        ""gmina"" : ""GmGłubczyce"",
			        ""powiat"" : ""PwGłubczyce"",
			        ""wojewodztwo"" : ""Opolskie"",
			        ""nip"" : ""7480007045"",
			        ""regon"" : ""530921155"",
			        ""nazwa2"" : ""Nazwa2xxx"",
			        ""pesel"" : ""123456"",
			        ""idoper"" : ""Administrator1"",
			        ""idakcept"" : ""Administrator2"",
			        ""nazwisko"" : ""jakiesnazwisko"",
			        ""imie"" : ""jakiesimie"",
			        ""datamodify"" : ""2017.08.26 14:35:14"",
			        ""dataakcept"" : ""2017.08.27 15:35:14"",
			        ""systembazowy"" : ""VFP"",
			        ""usuniety"" : ""0""
		            }";
        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToKatFirmyMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);

            KatFirmy firmy = this._Sut.Map(parsedJson);
            Assert.AreEqual("TFG", firmy.Firma);
            Assert.AreEqual("TOP FARMS GŁUBCZYCE SP. Z O.O.", firmy.Nazwa);
            Assert.AreEqual("TOP FARMS Głubczyce sp zoo", firmy.Nazwaskrocona);
            Assert.AreEqual("BOLESŁAWA CHROBREGO", firmy.Ulica);
            Assert.AreEqual("23", firmy.Numerdomu);
            Assert.AreEqual("abc", firmy.Numerlokalu);
            Assert.AreEqual("MGŁUBCZYCE", firmy.Miasto);
            Assert.AreEqual("48-100", firmy.Kodpocztowy);
            Assert.AreEqual("GŁUBCZYCE", firmy.Poczta);
            Assert.AreEqual("GmGłubczyce", firmy.Gmina);
            Assert.AreEqual("PwGłubczyce", firmy.Powiat);
            Assert.AreEqual("Opolskie", firmy.Wojewodztwo);
            Assert.AreEqual("7480007045", firmy.Nip);
            Assert.AreEqual("530921155", firmy.Regon);
            Assert.AreEqual("Nazwa2xxx", firmy.Nazwa2);
            Assert.AreEqual("123456", firmy.Pesel);
            Assert.AreEqual("Administrator1", firmy.Idoper);
            Assert.AreEqual("Administrator2", firmy.Idakcept);
            Assert.AreEqual("jakiesnazwisko", firmy.Nazwisko);
            Assert.AreEqual("jakiesimie", firmy.Imie);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:35:14"), firmy.Datamodify);
            Assert.AreEqual(DateTime.Parse("2017.08.27 15:35:14"), firmy.Dataakcept);
            Assert.AreEqual("VFP", firmy.Systembazowy);
            Assert.AreEqual(false, firmy.Usuniety);
            //Assert.AreEqual("zakrystia", firmy.Waitingroom);
        }
    }
}
