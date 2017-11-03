using Eteczka.BE.DTO;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Mappers
{
    public class PodWydzialDtoMapperTest
    {
        private IPodWydzialDtoMapper _Sut;

        [SetUp]
        public void SetUp()
        {
            this._Sut = new PodWydzialDtoMapper();
        }

        [Test]
        public void Map()
        {
            KatPodWydzialy podwydzialZBazy = new KatPodWydzialy()
                {
                    Dataakcept = DateTime.Parse("2011-11-11"),
                    Datamodify = DateTime.Parse("2012-12-12"),
                    Firma = "jakasFirma",
                    Idakcept = "IdAkcept1",
                    Idoper = "IdOper1",
                    Nazwa = "JakasNazwa",
                    Podwydzial = "JakisPodwydzial",
                    SystemBazowy = "JakisSystemBazowy",
                    Usuniety = false,
                    Wydzial = "JakisWydzial"
                };
            PodWydzialDTO zmapowany = _Sut.Mapuj(podwydzialZBazy);

            Assert.AreEqual(DateTime.Parse("2011-11-11"), zmapowany.Dataakcept);
            Assert.AreEqual(DateTime.Parse("2012-12-12"), zmapowany.Datamodify);
            Assert.AreEqual("jakasFirma", zmapowany.Firma);
            Assert.AreEqual("IdAkcept1", zmapowany.Idakcept);
            Assert.AreEqual("IdOper1", zmapowany.Idoper);
            Assert.AreEqual("JakasNazwa", zmapowany.Nazwa);
            Assert.AreEqual("JakisPodwydzial", zmapowany.Podwydzial);
            Assert.AreEqual("JakisSystemBazowy", zmapowany.SystemBazowy);
            Assert.AreEqual(false, zmapowany.Usuniety);
            Assert.AreEqual("JakisWydzial", zmapowany.Wydzial);
        }
    }
}
