using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToKatLokalMapperTest
    {
        private IJsonToKatLokalMapper _Sut;
        string FULL_VALID_JSON = @"{
			""firma"" : ""TFG"",
			""lokalpapier"" : ""LP"",
			""nazwa"" : ""TOP FARMS GŁUBCZYCE SP. Z O.O."",
			""ulica"" : ""BOLESŁAWA CHROBREGO"",
			""numerdomu"" : ""23"",
			""numerlokalu"" : ""45"",
			""miasto"" : ""GŁUBCZYCE"",
			""kodpocztowy"" : ""48-100"",
			""poczta"" : ""POCZGŁUBCZYCE"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""datamodify"" : ""2017.08.26 14:35:14"",
			""dataakcept"" : ""2017.11.26 18:35:14"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToKatLokalMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);

            KatLokalPapier lokalPapier = this._Sut.Map(parsedJson);
            Assert.AreEqual("TFG", lokalPapier.Firma);
            Assert.AreEqual("LP", lokalPapier.LokalPapier);
            Assert.AreEqual("TOP FARMS GŁUBCZYCE SP. Z O.O.", lokalPapier.Nazwa);
            Assert.AreEqual("BOLESŁAWA CHROBREGO", lokalPapier.Ulica);
            Assert.AreEqual("23", lokalPapier.Numerdomu);
            Assert.AreEqual("45", lokalPapier.Numerlokalu);
            Assert.AreEqual("GŁUBCZYCE", lokalPapier.Miasto);
            Assert.AreEqual("48-100", lokalPapier.Kodpocztowy);
            Assert.AreEqual("POCZGŁUBCZYCE", lokalPapier.Poczta);
            Assert.AreEqual("Administrator", lokalPapier.Idoper);
            Assert.AreEqual("Administrator2", lokalPapier.Idakcept);
            Assert.AreEqual(DateTime.Parse("2017.08.26 14:35:14"), lokalPapier.Datamodify);
            Assert.AreEqual(DateTime.Parse("2017.11.26 18:35:14"), lokalPapier.Dataakcept);
            Assert.AreEqual("VFP", lokalPapier.System);
            Assert.AreEqual(false, lokalPapier.Usuniety);
        }
    }
}
