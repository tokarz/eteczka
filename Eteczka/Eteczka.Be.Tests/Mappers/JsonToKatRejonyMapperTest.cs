using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToKatRejonyMapperTest
    {
        private IJsonToKatRejonyMapper _Sut;

        string FULL_VALID_JSON = @"{
			""rejon"" : ""02"",
			""nazwa"" : ""Bogdanowice"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""firma"" : ""TFG"",
			""datamodify"" : ""2017.11.26 14:35:22"",
			""dataakcept"" : ""2017.08.26 14:55:22"",
			""mnemonik"" : ""BG"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""      
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToKatRejonyMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            KatRejony rejon = _Sut.Map(parsedJson);

            Assert.AreEqual("02", rejon.Rejon);
            Assert.AreEqual("Bogdanowice", rejon.Nazwa);
            Assert.AreEqual("Administrator", rejon.Idoper);
            Assert.AreEqual("Administrator2", rejon.Idakcept);
            Assert.AreEqual("TFG", rejon.Firma);
            Assert.AreEqual(DateTime.Parse("2017.11.26 14:35:22"), rejon.Datamodify);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:55:22"), rejon.Dataakcept);
            Assert.AreEqual("BG", rejon.Mnemonik);
            Assert.AreEqual("VFP", rejon.Systembazowy);
            Assert.AreEqual(false, rejon.Usuniety);


        }
    }
}
