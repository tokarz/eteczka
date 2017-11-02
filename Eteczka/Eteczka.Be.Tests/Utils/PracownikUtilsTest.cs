using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Eteczka.BE.DTO;
using Eteczka.BE.Mappers;
using Eteczka.BE.Utils;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Tests.Utils
{

    [TestFixture]
    public class PracownikUtilsTest
    {
        private PracownikUtils _Sut;

        [SetUp]
        public void Init()
        {
            _Sut = new PracownikUtils();
        }






    }



}