using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToKatLokalMapperTest
    {
        private IJsonToKatLokalMapper _Sut;
        string FULL_VALID_JSON = @"{
			""firma"" : ""TFG"",
			""lokalpapier"" : ""TFG"",
			""nazwa"" : ""TOP FARMS GŁUBCZYCE SP. Z O.O."",
			""ulica"" : ""BOLESŁAWA CHROBREGO"",
			""numerdomu"" : ""23"",
			""numerlokalu"" : """",
			""miasto"" : ""GŁUBCZYCE"",
			""kodpocztowy"" : ""48-100"",
			""poczta"" : ""GŁUBCZYCE"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator"",
			""datamodify"" : ""2017.08.26 14:35:14"",
			""dataakcept"" : ""2017.08.26 14:35:14"",
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
        }
    }
}
