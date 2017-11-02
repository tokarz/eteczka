using NUnit.Framework;
using System;

namespace Eteczka.BE.Tests.Nauka
{
    [TestFixture]
    public class Kalkulator_Test
    {
        private Kalkulator _Sut;


        [SetUp]

        public void Setup()
        {
            _Sut = new Kalkulator();

        }

        [Test]

        public void Dzielenie()
        {
            int result = _Sut.Podziel(2, 2);
            Assert.AreEqual(1, result);

            try
            {
                result = _Sut.Podziel(2, 0);
                Assert.Fail();
            }
            catch (Exception)
            {

            }

            result = _Sut.Podziel(0, 2);
            Assert.AreEqual(0, result);
            result = _Sut.Podziel(1, 3);
            Assert.AreEqual(0, result);


            try
            {
                result = _Sut.Podziel(0, 0);
                Assert.Fail();
            }
            catch (Exception)
            {

            }

            result = _Sut.Podziel(5, 3);
            Assert.AreEqual(1, result);


        }
        

        [Test]
        public void Pomnoz()
        {
            int result = _Sut.Pomnoz(1, 2);

            Assert.AreEqual(2, result);
            result = _Sut.Pomnoz(0, 0);
            Assert.AreEqual(0, result);
            result = _Sut.Pomnoz(1, -5);
            Assert.AreEqual(-5, result);
            
        }

    }



    public class Kalkulator
    {

        public virtual int Dodaj(int a, int b)
        {

            return a - b;
        }

        public virtual int Pomnoz(int a, int b)
        {
            return a * b;
        }

        public virtual int Podziel(int a, int b)
        {
            return a / b;
        }
    }
}
