using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToWydzialMapperTest
    {
        private IJsonToWydzialMapper _Sut;
        string FULL_VALID_JSON = @"{
		    ""wydzial"" : ""94"",
			""nazwa"" : ""AKADEMIA TOP FARMS"",
			""datamodify"" : ""1899.12.30 14:35:22"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""dataakcept"" : ""1889.12.30 14:45:22"",
			""firma"" : ""TFG"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToWydzialMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            KatWydzialy wydzial = _Sut.Map(parsedJson);

            Assert.AreEqual("94", wydzial.Wydzial);
            Assert.AreEqual("AKADEMIA TOP FARMS", wydzial.Nazwa);
            Assert.AreEqual(DateTime.Parse("1899.12.30 14:35:22"), wydzial.Datamodify);
            Assert.AreEqual("Administrator", wydzial.Idoper);
            Assert.AreEqual("Administrator2", wydzial.Idakcept);
            Assert.AreEqual(DateTime.Parse("1889.12.30 14:45:22"), wydzial.Dataakcept);
            Assert.AreEqual("TFG", wydzial.Firma);
            Assert.AreEqual("VFP", wydzial.Systembazowy);
            Assert.AreEqual(false, wydzial.Usuniety);
        }
    }
}
