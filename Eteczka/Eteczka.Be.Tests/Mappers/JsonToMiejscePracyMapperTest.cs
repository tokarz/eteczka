using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToMiejscePracyMapperTest
    {
        private IJsonToMiejscePracyMapper _Sut;

        string FULL_VALID_JSON = @"{
			""firma"" : ""TFG"",
			""rejon"" : ""18"",
			""wydzial"" : ""25"",
			""podwydzial"" : ""bbb"",
			""konto5"" : ""11Z4202"",
			""datapocz"" : ""2013-02-01"",
			""datakoniec"" : ""2014-12-31"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""datamodify"" : ""2017.08.26 14:35:23"",
			""dataakcept"" : ""2017.08.27 15:35:23"",
			""numeread"" : ""UZUSRE76021519812"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0"",
			""id"" : ""1""    
		}";

        string FULL_VALID_JSON_WITH_WRONG_DATE = @"{
			""firma"" : ""TFG"",
			""rejon"" : ""18"",
			""wydzial"" : ""25"",
			""podwydzial"" : ""bbb"",
			""konto5"" : ""11Z4202"",
			""datapocz"" : ""2013-02-01"",
			""datakoniec"" : ""9999-99-99"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""datamodify"" : ""2017.08.26 14:35:23"",
			""dataakcept"" : ""2017.08.27 15:35:23"",
			""numeread"" : ""UZUSRE76021519812"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""1"",
			""id"" : ""1""    
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToMiejscePracyMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            MiejscePracy miejscePracy = _Sut.Map(parsedJson);

            Assert.AreEqual("TFG", miejscePracy.Firma);
            Assert.AreEqual("18", miejscePracy.Rejon);
            Assert.AreEqual("25", miejscePracy.Wydzial);
            Assert.AreEqual("bbb", miejscePracy.Podwydzial);
            Assert.AreEqual("11Z4202", miejscePracy.Konto5);
            Assert.AreEqual(DateTime.Parse("2013-02-01"), miejscePracy.DataPocz);
            Assert.AreEqual(DateTime.Parse("2014-12-31"), miejscePracy.DataKoniec);
            Assert.AreEqual("Administrator", miejscePracy.IdOper);
            Assert.AreEqual("Administrator2", miejscePracy.IdAkcept);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:35:23"), miejscePracy.DataModify);
            Assert.AreEqual(DateTime.Parse("2017.08.27 15:35:23"), miejscePracy.DataAkcept);
            Assert.AreEqual("UZUSRE76021519812", miejscePracy.NumerEad);
            Assert.AreEqual("VFP", miejscePracy.SystemBazowy);
            Assert.AreEqual(false, miejscePracy.Usuniety);
            Assert.AreEqual(1, miejscePracy.Id);
        }

        [Test]
        public void Map_DataKoniec_I_Usuniety()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON_WITH_WRONG_DATE);
            MiejscePracy miejscePracy = _Sut.Map(parsedJson);

            Assert.AreEqual("TFG", miejscePracy.Firma);
            Assert.AreEqual("18", miejscePracy.Rejon);
            Assert.AreEqual("25", miejscePracy.Wydzial);
            Assert.AreEqual("bbb", miejscePracy.Podwydzial);
            Assert.AreEqual("11Z4202", miejscePracy.Konto5);
            Assert.AreEqual(DateTime.Parse("2013-02-01"), miejscePracy.DataPocz);
            Assert.AreEqual(DateTime.MaxValue.Date, miejscePracy.DataKoniec);
            Assert.AreEqual("Administrator", miejscePracy.IdOper);
            Assert.AreEqual("Administrator2", miejscePracy.IdAkcept);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:35:23"), miejscePracy.DataModify);
            Assert.AreEqual(DateTime.Parse("2017.08.27 15:35:23"), miejscePracy.DataAkcept);
            Assert.AreEqual("UZUSRE76021519812", miejscePracy.NumerEad);
            Assert.AreEqual("VFP", miejscePracy.SystemBazowy);
            Assert.AreEqual(true, miejscePracy.Usuniety);
            Assert.AreEqual(1, miejscePracy.Id);
        }
    }
}
