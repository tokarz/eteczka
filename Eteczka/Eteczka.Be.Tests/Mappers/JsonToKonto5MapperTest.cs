using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class JsonToKonto5MapperTest
    {
        private IJsonToKonto5Mapper _Sut;

        string FULL_VALID_JSON = @"{
			""konto5"" : ""11D0000"",
			""nazwa"" : ""Koszty ogólnego zarządu"",
			""idoper"" : ""Administrator"",
			""idakcept"" : ""Administrator2"",
			""firma"" : ""TFG"",
			""kontoskr"" : ""D0000"",
			""datamodify"" : ""2017.08.26 16:35:22"",
			""dataakcept"" : ""2017.08.26 15:35:22"",
			""systembazowy"" : ""VFP"",
			""usuniety"" : ""0""  
		}";

        [SetUp]
        public void SetUp()
        {
            this._Sut = new JsonToKonto5Mapper();
        }

        [Test]
        public void Map()
        {
            JToken parsedJson = JToken.Parse(FULL_VALID_JSON);
            KatKonto5 konto5 = _Sut.Map(parsedJson);

            Assert.AreEqual("11D0000", konto5.Konto5);
            Assert.AreEqual("Koszty ogólnego zarządu", konto5.Nazwa);
            Assert.AreEqual("Administrator", konto5.Idoper);
            Assert.AreEqual("Administrator2", konto5.Idakcept);
            Assert.AreEqual("TFG", konto5.Firma);
            Assert.AreEqual("D0000", konto5.Kontoskr);
            Assert.AreEqual(DateTime.Parse("2017.08.26 16:35:22"), konto5.Datamodify);
            Assert.AreEqual(DateTime.Parse("2017.08.26 15:35:22"), konto5.Dataakcept);
            Assert.AreEqual("VFP", konto5.Systembazowy);
            Assert.AreEqual(false, konto5.Usuniety);
        }
    }
}
