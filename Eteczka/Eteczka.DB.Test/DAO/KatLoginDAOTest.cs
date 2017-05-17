﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Collections.Generic;
using Eteczka.DB.Entities;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.Connection;

namespace Eteczka.DB.DAO
{
    [TestFixture]
    public class KatLoginDAOTest
    {
        private KatLoginDAO _Sut;

        [SetUp]
        public void setUp()
        {
            _Sut = new KatLoginDAO(new DbConnectionFactory(new ConnectionDetails("postgres", "admin", "localhost", "5432", "E-Agropin-EAD")));
        }

        [Test]
        public void WczytajPracownikaPoNazwieIHasle()
        {
            List<KatLoginy> result = _Sut.WczytajPracownikaPoNazwieIHasle("M.Tokarz", "tokitoki");
            Assert.IsNotNull(result);
            Assert.AreEqual("Maciej", result[0].Imie.Trim());
            Assert.AreEqual("Maciej", result[1].Imie.Trim());
            Assert.AreEqual("AFM", result[0].FirmaSymbol.Trim());
            Assert.AreEqual("JAG", result[1].FirmaSymbol.Trim());
        }


    }
}
