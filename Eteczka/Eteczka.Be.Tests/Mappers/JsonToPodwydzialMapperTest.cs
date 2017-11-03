using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToPodwydzialMapperTest
    {
        private IJsonToPodwydzialMapper _Sut;
        string FULL_VALID_JSON = @"{
			""podwydzial"" : ""06"",
			""nazwa"" : ""Bydło mięsne Mikoszki"",
			""wydzial"" : ""05"",
			""datamodify"" : ""1879.12.30 14:41:25"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""dataakcept"" : ""1899.12.30 14:41:25"",
			""firma"" : ""TFW"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""    
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToPodwydzialMapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            KatPodWydzialy podwydzial = _Sut.Map(parsedJson);

            Assert.AreEqual("06", podwydzial.Podwydzial);
            Assert.AreEqual("Bydło mięsne Mikoszki", podwydzial.Nazwa);
            Assert.AreEqual("05", podwydzial.Wydzial);
            Assert.AreEqual(DateTime.Parse("1879.12.30 14:41:25"), podwydzial.Datamodify);
            Assert.AreEqual("Administrator", podwydzial.Idoper);
            Assert.AreEqual("Administrator2", podwydzial.Idakcept);
            Assert.AreEqual(DateTime.Parse("1899.12.30 14:41:25"), podwydzial.Dataakcept);
            Assert.AreEqual("TFW", podwydzial.Firma);
            Assert.AreEqual("VFP", podwydzial.SystemBazowy);
            Assert.AreEqual(false, podwydzial.Usuniety);
        }
    }
}
