using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.DB.DAO;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Services
{
    [TestFixture]
    public class PlikiServiceTest
    {

        //private PlikiService _Sut;
        //private PlikiDAO _PlikDao;
        //private PlikiUtils _PlikiUtils;

        [SetUp]
        public void setUp()
        {
            //_PlikDao = Substitute.For<PlikiDAO>();
            //_PlikiUtils = Substitute.For<PlikiUtils>();

            //_Sut = new PlikiService(_PlikDao, _PlikiUtils);
        }

        [Test]
        public void foo()
        {
            object[] args = new object[] {
                "A",
                "B"
            };
            try
            {
                string values = string.Format("{0}, {1}", args);
            }
            catch(Exception ex)
            {
                Assert.Fail();
            }
        }

    }
}
